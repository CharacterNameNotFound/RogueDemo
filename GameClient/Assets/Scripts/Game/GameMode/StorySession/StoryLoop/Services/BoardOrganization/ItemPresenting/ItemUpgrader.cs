using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using UnityEngine;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public class ItemUpgrader : IItemUpgrader
    {
        private IItemPresenter _itemPresenter;
        private GameBoardHolder _gameBoardHolder;
        private IItemRemover _itemRemover;
        private ItemUpgraderConfig _itemUpgraderConfig;
        private IItemRegistry _itemRegistry;
        private IItemStatGetter _statGetter;

        public ItemUpgrader(
            IItemPresenter itemPresenter, 
            GameBoardHolder gameBoardHolder, 
            IItemRemover itemRemover, 
            ItemUpgraderConfig itemUpgraderConfig, 
            IItemRegistry itemRegistry, 
            IItemStatGetter statGetter)
        {
            _itemPresenter = itemPresenter;
            _gameBoardHolder = gameBoardHolder;
            _itemRemover = itemRemover;
            _itemUpgraderConfig = itemUpgraderConfig;
            _itemRegistry = itemRegistry;
            _statGetter = statGetter;
        }
        
        public async UniTask PlayUpgrade(
            ItemContainerComponent upgradableItem,
            ItemContainerComponent targetItem,
            ItemLineComponent upgradableItemLine,
            CancellationToken cancellationToken)
        {
            // starting loading before animation
            UniTask<RequestResult<Item>> upgradedItemLoading = _itemRegistry.GetOrLoadById(upgradableItem.StoredItem.UpgradedItemId, cancellationToken);


            Vector3 originalPosition = upgradableItem.transform.position;
            
            Vector3 startPosition = upgradableItem.transform.position;
            
            // we're changing animation a little if item placed in stash 
            if (upgradableItemLine == _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine)
            {
                startPosition = _gameBoardHolder.GameBoardComponent.GameBoardInteractables.InventoryButton.transform.position;
                
                upgradableItem.transform.position = startPosition;
            }

            //setting up rendering
            upgradableItem.RenderUpgradeMovement(1);
            targetItem.RenderUpgradeMovement();
            
            upgradableItem.transform.SetParent(null);
            targetItem.transform.SetParent(null);

            // As both item animations have same duration, we can just wait for second one
            GetToUpgradePositionSequence(upgradableItem.transform, _itemUpgraderConfig.UpgradedItemPosition).Play().WithCancellation(cancellationToken).Forget();
            await GetToUpgradePositionSequence(targetItem.transform, _itemUpgraderConfig.TargetItemPosition).Play().WithCancellation(cancellationToken);
            
            
            Item preservedItem = targetItem.StoredItem;
            
            // playing clash + roll animation
            await GetItemClashSequence(upgradableItem.transform, targetItem.transform).Play();
            targetItem.transform.localScale = Vector3.one;
            _itemRemover.RemoveItem(targetItem);

            await upgradableItem.transform.DORotate(new Vector3(0, 270, 0), _itemUpgraderConfig.PreUpgradeRotationTime, RotateMode.LocalAxisAdd).Play().WithCancellation(cancellationToken);
            
            // upgrading an item
            RequestResult<Item> upgradedItem = await upgradedItemLoading;
            upgradableItem.StoredItem = upgradedItem.GetValue();
            _itemPresenter.UpdateItemRarityFrame(upgradableItem);
            
            // upgrading item stats
            UpdateStats(upgradableItem.StoredItem, preservedItem);
            float value = _statGetter.GetStatValue(
                upgradableItem.StoredItem, 
                ItemStatType.Value, 
                StatSet.StatSetComponent.Special,
                StatSet.StatSetComponent.Special);
            upgradableItem.SetPriceTag(value);
            
            
            await upgradableItem.transform.DORotate(new Vector3(0, 90, 0), _itemUpgraderConfig.PostUpgradeRotationTime, RotateMode.LocalAxisAdd).Play().WithCancellation(cancellationToken);
            
            await GetToOriginalPositionSequence(upgradableItem.transform, startPosition).Play().WithCancellation(cancellationToken);
            
            
            upgradableItem.transform.SetParent(upgradableItemLine.transform);
            upgradableItem.ResetRender();
            upgradableItem.transform.position = originalPosition;
        }
        
        public void UpdateStats(Item newItem, Item oldItem)
        {
            foreach (KeyValuePair<ItemStatType, ItemStatEntry> stat in newItem.ItemStats.Stats)
            {
                stat.Value.ItemValues.CopyUpgradeConsistentStats(oldItem.ItemStats.Stats[stat.Key].ItemValues);
                stat.Value.ItemPercentiles.CopyUpgradeConsistentStats(oldItem.ItemStats.Stats[stat.Key].ItemPercentiles);
            }

            newItem.ItemStats.CurrentCharge = oldItem.ItemStats.CurrentCharge;
        }

        private Sequence GetToUpgradePositionSequence(Transform transform, Vector3 targetPosition)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(targetPosition, _itemUpgraderConfig.PreUpgradeMovementTime));
            sequence.Join(transform.DORotate(_itemUpgraderConfig.PreUpgradeMovementRotation, _itemUpgraderConfig.PreUpgradeMovementTime, RotateMode.LocalAxisAdd));
            sequence.Join(transform.DOScale(_itemUpgraderConfig.IncreasedItemSize, _itemUpgraderConfig.PreUpgradeMovementTime));
            
            return sequence;
        }

        private Sequence GetItemClashSequence(Transform upgradedItem, Transform targetItem)
        {
            Vector3 centerPoint = Vector3.Lerp(_itemUpgraderConfig.UpgradedItemPosition, _itemUpgraderConfig.TargetItemPosition, 0.5f);
            
            Sequence sequence = DOTween.Sequence();

            sequence.Append(upgradedItem.DOMove(centerPoint, _itemUpgraderConfig.ItemClashTime));
            sequence.Join(targetItem.DOMove(centerPoint, _itemUpgraderConfig.ItemClashTime));

            return sequence;
        }

        private Sequence GetToOriginalPositionSequence(Transform transform, Vector3 targetPosition)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(targetPosition, _itemUpgraderConfig.ItemReturnTime));
            sequence.Join(transform.DOScale(Vector3.one, _itemUpgraderConfig.ItemReturnTime));
            
            return sequence;
        }
        
    }
}