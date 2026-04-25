using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Simulation.Facades;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public class ItemPresenter : IItemPresenter
    {
        private IItemContainersManager _containersManager;
        private IItemRegistry _itemRegistry;
        private IItemPresenterConfigs _itemPresenterConfigs;
        private IItemLineOrganizer _itemLineOrganizer;
        private GameBoardHolder _gameBoardHolder;
        private IItemRenderingFacade _itemRenderingFacade;

        private Sprite[] _frameSprites;
        
        public ItemPresenter(
            IItemContainersManager containersManager, 
            IItemRegistry itemRegistry, 
            IItemPresenterConfigs itemPresenterConfigs, 
            IItemLineOrganizer itemLineOrganizer, 
            GameBoardHolder gameBoardHolder, 
            IItemRenderingFacade itemRenderingFacade)
        {
            _containersManager = containersManager;
            _itemRegistry = itemRegistry;
            _itemPresenterConfigs = itemPresenterConfigs;
            _itemLineOrganizer = itemLineOrganizer;
            _gameBoardHolder = gameBoardHolder;
            _itemRenderingFacade = itemRenderingFacade;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            _frameSprites = new Sprite[5];

            _frameSprites[0] = await _itemPresenterConfigs.BronzeFrame.Load<Sprite>(cancellationToken);
            _frameSprites[1] = await _itemPresenterConfigs.SilverFrame.Load<Sprite>(cancellationToken);
            _frameSprites[2] = await _itemPresenterConfigs.GoldenFrame.Load<Sprite>(cancellationToken);
            _frameSprites[3] = await _itemPresenterConfigs.DiamondFrame.Load<Sprite>(cancellationToken);
            _frameSprites[4] = await _itemPresenterConfigs.LegendaryFrame.Load<Sprite>(cancellationToken);
        }
        
        public async UniTask ShowItems(IEnumerable<string> itemIds, CancellationToken cancellationToken)
        {
            List<ItemContainerComponent> itemContainers = new List<ItemContainerComponent>();
            
            foreach (string itemId in itemIds)
            {
                UniTask<RequestResult<Item>> loadItem = _itemRegistry.GetOrLoadById(itemId, cancellationToken);

                RequestResult<Item> requestResult = await loadItem;

                if (requestResult.IsFailure())
                {
                    throw requestResult.Exception;
                }

                Item item = requestResult.GetValue();
                
                ItemContainerComponent itemContainer = await _containersManager.GetBySize(item.ItemSize, cancellationToken);
                itemContainers.Add(itemContainer);
                itemContainer.StoredItem = item;

                itemContainer.FrameRenderer.sprite = _frameSprites[(int)item.ItemRarity];
                itemContainer.ItemRenderer.sprite = await item.ItemSpriteRuntimeKey.Load<Sprite>(cancellationToken);
                
                _itemRenderingFacade.UpdateCharge(itemContainer, 1);
            }

            ItemContainerComponent[] itemLineConfiguration = new ItemContainerComponent[12];
            int i = 0;
            foreach (ItemContainerComponent itemContainer in itemContainers)
            {
                itemContainer.gameObject.SetActive(true);
                itemContainer.transform.SetParent(_gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine.transform);
                
                for (int j = 0; j < itemContainer.Size; j++)
                {
                    itemLineConfiguration[i] = itemContainer;
                    i++;
                }
            }

            _itemLineOrganizer.Organize(_gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine, itemLineConfiguration, true);

        }

        public void RemoveItemsImmediate(IEnumerable<ItemContainerComponent> itemContainerComponents)
        {
            foreach (ItemContainerComponent container in itemContainerComponents)
            {
                _containersManager.Return(container);
            }
        }

        public void RemoveEncounterItemsImmediate()
        {
            ItemContainerComponent[] itemContainerComponents = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine.ItemContainerComponents;

            
            for (int i = 0; i < itemContainerComponents.Length;)
            {
                if (itemContainerComponents[i] is null)
                {
                    i++;
                    continue;
                }

                int step = itemContainerComponents[i].Size;
                Addressables.Release(itemContainerComponents[i].ItemRenderer.sprite);
                _containersManager.Return(itemContainerComponents[i]);
                
                for (int j = 0; j < step; j++)
                {
                    itemContainerComponents[j] = null;
                }
                
                i += step;
            }
            
        }

        public void CleanUp()
        {
            foreach (Sprite item in _frameSprites)
            {
                Addressables.Release(item);
            }

            _frameSprites = null;
        }
    }
}