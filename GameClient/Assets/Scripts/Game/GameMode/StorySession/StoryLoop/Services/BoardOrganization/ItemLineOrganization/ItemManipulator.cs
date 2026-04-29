using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using UnityEngine;


namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    // ToDo: look into reducing number of arguments in methods, refactoring nesting
    public class ItemManipulator : IItemManipulator
    {
        private IItemLineOrganizer _lineOrganizer;
        private GameBoardHolder _gameBoardHolder;
        private IItemTransactionOperationController _itemTransactionOperationController;
        private IItemContainersManager _containersManager;
        private IItemTransactionEventPublisher _itemTransactionEventPublisher;


        public ItemManipulator(
            IItemLineOrganizer lineOrganizer, 
            GameBoardHolder gameBoardHolder, 
            IItemTransactionOperationController itemTransactionOperationController, 
            IItemContainersManager containersManager, 
            IItemTransactionEventPublisher itemTransactionEventPublisher)
        {
            _lineOrganizer = lineOrganizer;
            _gameBoardHolder = gameBoardHolder;
            _itemTransactionOperationController = itemTransactionOperationController;
            _containersManager = containersManager;
            _itemTransactionEventPublisher = itemTransactionEventPublisher;
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


        public async UniTask<bool> TryCompleteItemTransition(
            Vector3 worldPosition,
            int targetItemOriginalIndex,
            ItemLineComponent originalLine,
            ItemLineComponent targetLine,
            ItemLineBuffer targetLineBuffer,
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer,
            ItemLineBuffer secondWorkerItemLineBuffer,
            CancellationToken cancellationToken)
        {
            bool isPurchaseRequired = _itemTransactionOperationController.IsPurchaseAttempt(item, originalLine);

            if (isPurchaseRequired && !_itemTransactionOperationController.CanPurchase(item, originalLine))
            {
                return false;
            }
            
            bool isUpgradeAttempt = _itemTransactionOperationController.CanUpgrade(item, originalLine);

            if (isUpgradeAttempt)
            {
                return await TryUpgrade(
                    worldPosition,
                    isPurchaseRequired,
                    targetItemOriginalIndex, 
                    originalLine, targetLine, 
                    targetLineBuffer, 
                    item, 
                    workerItemLineBuffer, 
                    secondWorkerItemLineBuffer, 
                    cancellationToken);
            }
            
            bool canUpdate = TryBuildItemConfiguration(
                worldPosition, 
                originalLine, 
                targetLine, 
                targetLineBuffer, 
                item, 
                workerItemLineBuffer, 
                secondWorkerItemLineBuffer, 
                targetItemOriginalIndex);

            if (!canUpdate)
            {
                return await TryStash(
                    worldPosition,
                    isPurchaseRequired,
                    targetItemOriginalIndex, 
                    originalLine, 
                    targetLine, 
                    targetLineBuffer, 
                    item,
                    workerItemLineBuffer, 
                    secondWorkerItemLineBuffer, 
                    cancellationToken);
            }
            
            return await TryMove(
                worldPosition, 
                isPurchaseRequired,
                targetItemOriginalIndex, 
                originalLine, 
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
            
            if (!TryBuildItemConfiguration(worldPosition, original, targetLine, targetLineBuffer, item, workerItemLineBuffer, null, -1))
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

            await _itemTransactionEventPublisher.HandlePreItemSell(item, cancellationToken);

            _lineOrganizer.RemoveItem(itemLine, item, out _);
            Item itemStoredItem = item.StoredItem;
            _containersManager.Return(item);
            
            await _itemTransactionEventPublisher.HandlePostItemSell(itemStoredItem, cancellationToken);
            
            return true;
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
            int targetItemOriginalIndex)
        {
            if (!_lineOrganizer.TryGetLineIndexForPosition(targetLine, worldPosition, out int index))
            {
                return false;
            }
            
            workerItemLineBuffer.ClearBuffer();
            bool isItemConfigurationBuilt = _lineOrganizer.TryBuildItemConfiguration(targetLineBuffer.ItemBuffer, item, ref index, workerItemLineBuffer.ItemBuffer);

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
        
        
        private async UniTask<bool> TryUpgrade(Vector3 worldPosition,
            bool isPurchaseRequired,
            int targetItemOriginalIndex,
            ItemLineComponent originalLine,
            ItemLineComponent targetLine,
            ItemLineBuffer targetLineBuffer,
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer,
            ItemLineBuffer secondWorkerItemLineBuffer,
            CancellationToken cancellationToken)
        {
            // will take payment
            if (isPurchaseRequired)
            {
                await _itemTransactionEventPublisher.HandlePreItemPurchase(item, cancellationToken);
            }

            await _itemTransactionEventPublisher.HandlePreItemUpgrade(item, cancellationToken);

            // ToDo: Play Upgrade Animation

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
            
            await _itemTransactionEventPublisher.HandlePreItemMovement(item, cancellationToken);
            
            
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

        private async UniTask<bool> TryStash(Vector3 worldPosition,
            bool isPurchaseRequired,
            int targetItemOriginalIndex,
            ItemLineComponent originalLine,
            ItemLineComponent targetLine,
            ItemLineBuffer targetLineBuffer,
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer,
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

            await _itemTransactionEventPublisher.HandlePreItemMovement(item, cancellationToken);
            
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