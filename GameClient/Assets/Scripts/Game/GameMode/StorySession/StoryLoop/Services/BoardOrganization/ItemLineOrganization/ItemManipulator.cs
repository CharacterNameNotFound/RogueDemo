using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.Utilities.EventArguments;
using UnityEngine;


namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    // ToDo: look into reducing number of arguments in methods, refactoring nesting
    public class ItemManipulator : IItemManipulator
    {
        private IItemLineOrganizer _lineOrganizer;
        private GameBoardHolder _gameBoardHolder;
        private IItemTransactionOperationController _itemTransactionOperationController;
        private IItemTransactionEventPublisher _itemTransactionEventPublisher;
        private IItemUpgrader _itemUpgrader;
        private IItemRemover _itemRemover;

        public ItemManipulator(
            IItemLineOrganizer lineOrganizer, 
            GameBoardHolder gameBoardHolder, 
            IItemTransactionOperationController itemTransactionOperationController, 
            IItemTransactionEventPublisher itemTransactionEventPublisher, 
            IItemUpgrader itemUpgrader, 
            IItemRemover itemRemover)
        {
            _lineOrganizer = lineOrganizer;
            _gameBoardHolder = gameBoardHolder;
            _itemTransactionOperationController = itemTransactionOperationController;
            _itemTransactionEventPublisher = itemTransactionEventPublisher;
            _itemUpgrader = itemUpgrader;
            _itemRemover = itemRemover;
        }
        

        public bool TryGetItemLineForItem(ItemContainerComponent item, out ItemLineComponent line)
        {
            foreach (ItemLineComponent itemLine in _gameBoardHolder.GameBoardComponent.ItemLineViewController.EnumerateItemLines())
            {
                if (!_lineOrganizer.IsLocatedInItemLine(itemLine, item.transform.position)) 
                    continue;
                
                line = itemLine;
                return true;
            }

            line = null;
            return false;
        }


        public async UniTask<bool> TryCompleteItemTransitionBetweenLines(
            Vector3 worldPosition,
            int targetItemOriginalIndex,
            ItemLineComponent originalItemLine,
            ItemLineComponent targetLine,
            ItemLineBuffer targetLineBuffer,
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer,
            ItemLineBuffer secondWorkerItemLineBuffer,
            CancellationToken cancellationToken)
        {
            bool isPurchaseRequired = _itemTransactionOperationController.IsPurchaseAttempt(item, originalItemLine);

            if (isPurchaseRequired && !_itemTransactionOperationController.CanPurchase(item, originalItemLine))
            {
                return false;
            }
            
            bool isUpgradeAttempt = _itemTransactionOperationController.CanUpgrade(
                item, 
                originalItemLine, 
                out ItemLineComponent upgradableItemLine, 
                out ItemContainerComponent upgradableItem);

            if (isUpgradeAttempt)
            {
                return await TryUpgrade(
                    isPurchaseRequired,
                    originalItemLine, 
                    item, 
                    upgradableItemLine, 
                    upgradableItem,
                    cancellationToken);
            }
            
            bool canUpdate = TryBuildItemConfiguration(
                worldPosition, 
                originalItemLine, 
                targetLine, 
                targetLineBuffer, 
                item, 
                workerItemLineBuffer, 
                secondWorkerItemLineBuffer, 
                targetItemOriginalIndex,
                out int newIndex);

            if (!canUpdate)
            {
                return await TryStash(
                    isPurchaseRequired,
                    targetItemOriginalIndex, 
                    originalItemLine, 
                    item,
                    secondWorkerItemLineBuffer, 
                    cancellationToken);
            }
            
            return await TryMove(
                worldPosition, 
                isPurchaseRequired,
                targetItemOriginalIndex, 
                newIndex,
                originalItemLine, 
                targetLine, 
                targetLineBuffer, 
                item, 
                workerItemLineBuffer, 
                secondWorkerItemLineBuffer, 
                cancellationToken);

        }
        

        public bool TryUpdateItemLines(
            Vector3 worldPosition, 
            ItemLineComponent original, 
            ItemLineComponent targetLine, 
            ItemLineBuffer targetLineBuffer, 
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer)
        {
            
            if (!TryBuildItemConfiguration(worldPosition, original, targetLine, targetLineBuffer, item, workerItemLineBuffer, null, -1, out _))
            {
                return false;
            }
            
            _lineOrganizer.Organize(targetLine, workerItemLineBuffer.ItemBuffer, true);
            
            return true;
        }

        public async UniTask<bool> TrySellItem(Vector3 mouseWorldPoint,
            ItemContainerComponent[] itemLine,
            ItemLineComponent originalItemLine,
            ItemContainerComponent item,
            CancellationToken cancellationToken)
        {
            if (!_itemTransactionOperationController.CanSellItem(item, originalItemLine))
            {
                return false;
            }

            Collider2D collider = Physics2D.OverlapPoint(mouseWorldPoint);
            if (collider is null || 
                collider.gameObject != _gameBoardHolder.GameBoardComponent.EncounterBoard.SellFirmRenderer.gameObject)
            {
                return false;
            }

            await _itemTransactionOperationController.SellItem(item, itemLine, cancellationToken);
            
            return true;
        }

        public UniTask<bool> TryCompleteItemTransitionToStash(
            int originalIndex,
            ItemLineComponent originalItemLine, 
            ItemContainerComponent targetItem,
            ItemLineBuffer workerItemLineBuffer,
            CancellationToken cancellationToken)
        {
            bool isPurchaseRequired = _itemTransactionOperationController.IsPurchaseAttempt(targetItem, originalItemLine);

            if (isPurchaseRequired && !_itemTransactionOperationController.CanPurchase(targetItem, originalItemLine))
            {
                return UniTask.FromResult(false);
            }
            
            bool isUpgradeAttempt = _itemTransactionOperationController.CanUpgrade(
                targetItem, 
                originalItemLine, 
                out ItemLineComponent upgradableItemLine, 
                out ItemContainerComponent upgradableItem);

            if (isUpgradeAttempt)
            {
                return  TryUpgrade(
                    isPurchaseRequired,
                    originalItemLine, 
                    targetItem, 
                    upgradableItemLine, 
                    upgradableItem,
                    cancellationToken);
            }
            
            return TryStash(
                isPurchaseRequired,
                originalIndex, 
                originalItemLine, 
                targetItem,
                workerItemLineBuffer, 
                cancellationToken);
        }

        /// <summary>
        /// If second worker is null, no attempt on swap will be done
        /// </summary>
        /// <param name="secondWorkerItemLineBuffer"> If second worker is null, no attempt on swap will be done</param>
        /// <param name="targetItemOriginalIndex"> Required only for swap </param>
        private bool TryBuildItemConfiguration(Vector3 worldPosition,
            ItemLineComponent original,
            ItemLineComponent targetLine,
            ItemLineBuffer targetLineBuffer,
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer,
            ItemLineBuffer secondWorkerItemLineBuffer, 
            int targetItemOriginalIndex,
            out int newIndex)
        {
            if (!_lineOrganizer.TryGetLineIndexForPosition(targetLine, worldPosition, out newIndex))
            {
                return false;
            }
            
            workerItemLineBuffer.ClearBuffer();
            bool isItemConfigurationBuilt = _lineOrganizer.TryBuildItemConfiguration(targetLineBuffer.ItemBuffer, item, ref newIndex, workerItemLineBuffer.ItemBuffer);

            if (isItemConfigurationBuilt)
            {
                return true;
            }

            if (secondWorkerItemLineBuffer is null)
            {
                return false;
            }

            if (!targetLine.IsPlayerModifyAvailable || !original.IsPlayerModifyAvailable)
            {
                return false;
            }

            if (original == targetLine)
            {
                return false;
            }
            
            workerItemLineBuffer.ClearBuffer();
            secondWorkerItemLineBuffer.ClearBuffer();

            return _lineOrganizer.TryMakeSwap(
                worldPosition,
                targetItemOriginalIndex,
                original, 
                targetLine, 
                item,
                secondWorkerItemLineBuffer.ItemBuffer,
                workerItemLineBuffer.ItemBuffer);
        }
        
        
        private async UniTask<bool> TryUpgrade(
            bool isPurchaseRequired,
            ItemLineComponent originalLine,
            ItemContainerComponent item,
            ItemLineComponent upgradableItemLine, 
            ItemContainerComponent upgradableItem,
            CancellationToken cancellationToken)
        {
            // will take payment
            if (isPurchaseRequired)
            {
                await _itemTransactionEventPublisher.HandlePreItemPurchase(item, cancellationToken);
            }

            await _itemTransactionEventPublisher.HandlePreItemUpgrade(item, cancellationToken);

            await _itemUpgrader.PlayUpgrade(upgradableItem, item, upgradableItemLine, cancellationToken);

            await _itemTransactionEventPublisher.HandlePostItemUpgrade(item, cancellationToken);
            
            if (isPurchaseRequired)
            {
                await _itemTransactionEventPublisher.HandlePostItemPurchase(item, cancellationToken);
            }

            return true;
        }

        private async UniTask<bool> TryMove(Vector3 worldPosition,
            bool isPurchaseRequired,
            int targetItemOriginalIndex,
            int newIndex,
            ItemLineComponent originalLine,
            ItemLineComponent targetLine,
            ItemLineBuffer targetLineBuffer,
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer,
            ItemLineBuffer secondWorkerItemLineBuffer,
            CancellationToken cancellationToken)
        {
            if (isPurchaseRequired)
            {
                await _itemTransactionEventPublisher.HandlePreItemPurchase(item, cancellationToken);
            }

            PreItemMovementArguments preItemMovementArguments = new PreItemMovementArguments(
                item, 
                originalLine, 
                targetItemOriginalIndex);
            
            await _itemTransactionEventPublisher.HandlePreItemMovement(preItemMovementArguments, cancellationToken);
            
            
            if (secondWorkerItemLineBuffer.HasValue())
            {
                _lineOrganizer.Organize(originalLine, secondWorkerItemLineBuffer.ItemBuffer, true);
                ReparentItemContainers(originalLine);
            }
            
            _lineOrganizer.Organize(targetLine, workerItemLineBuffer.ItemBuffer, true);
            item.transform.SetParent(targetLine.transform);
            
            
            await _itemTransactionEventPublisher.HandlePostItemMovement(item, cancellationToken);

            if (isPurchaseRequired)
            {
                await _itemTransactionEventPublisher.HandlePostItemPurchase(item, cancellationToken);
            }
            

            return true;
        }

        private async UniTask<bool> TryStash(
            bool isPurchaseRequired,
            int originalIndex,
            ItemLineComponent originalLine,
            ItemContainerComponent item,
            ItemLineBuffer secondWorkerItemLineBuffer,
            CancellationToken cancellationToken)
        {
            // trying to put into stash
            if (originalLine != _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine)
            {
                return false;
            }

            ItemLineComponent stashLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine;
                
            // check for space in stash
            if (stashLine.ItemContainerComponents.Count(x => x is null) < item.Size)
            {
                return false;
            }
            
            //stashing

            if (isPurchaseRequired)
            {
                await _itemTransactionEventPublisher.HandlePreItemPurchase(item, cancellationToken);
            }

            PreItemMovementArguments arguments = new PreItemMovementArguments(item, originalLine, originalIndex);
            
            await _itemTransactionEventPublisher.HandlePreItemMovement(arguments, cancellationToken);
            
            int firstEmpty = Array.IndexOf(stashLine.ItemContainerComponents, null);
            _lineOrganizer.TryBuildItemConfiguration(stashLine, item, ref firstEmpty, secondWorkerItemLineBuffer.ItemBuffer);
            _lineOrganizer.Organize(stashLine, secondWorkerItemLineBuffer.ItemBuffer, true);

            ReparentItemContainers(stashLine);
            
            await _itemTransactionEventPublisher.HandlePostItemMovement(item, cancellationToken);
            
            if (isPurchaseRequired)
            {
                await _itemTransactionEventPublisher.HandlePostItemPurchase(item, cancellationToken);
            }

            return true;
        }

        // ToDo: it is heavy-handed, but will save some time for now 
        private void ReparentItemContainers(ItemLineComponent itemLineComponent)
        {
            for (int i = 0; i < itemLineComponent.ItemContainerComponents.Length;)
            {
                if (itemLineComponent.ItemContainerComponents[i] is null)
                {
                    i++;
                    continue;
                }

                int size = itemLineComponent.ItemContainerComponents[i].Size;
                for (int j = 0; j < size; j++)
                {
                    itemLineComponent.ItemContainerComponents[i + j].transform.SetParent(itemLineComponent.transform);
                }

                i += size;
            }
            
            
        }
        
        
    }
}