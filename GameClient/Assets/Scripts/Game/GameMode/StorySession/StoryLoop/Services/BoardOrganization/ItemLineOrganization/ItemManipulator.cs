using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    // ToDo: look into reducing number of arguments in methods
    public class ItemManipulator : IItemManipulator
    {
        private IItemLineOrganizer _lineOrganizer;
        private GameBoardHolder _gameBoardHolder;
        private IEncounterPlayer _encounterPlayer;

        public ItemManipulator(IItemLineOrganizer lineOrganizer, GameBoardHolder gameBoardHolder, IEncounterPlayer encounterPlayer)
        {
            _lineOrganizer = lineOrganizer;
            _gameBoardHolder = gameBoardHolder;
            _encounterPlayer = encounterPlayer;
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
            bool canUpdate = TryBuildItemConfiguration(worldPosition, originalLine, targetLine, targetLineBuffer, item, workerItemLineBuffer, secondWorkerItemLineBuffer, targetItemOriginalIndex);

            if (!canUpdate)
            {
                return false;
            }

            if (originalLine == _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine)
            {
                bool canMoveItem = _encounterPlayer.CanMoveItem(item);
                if (!canMoveItem)
                {
                    return false;
                }

                await _encounterPlayer.HandlePreItemMove(item, cancellationToken);
            }

            if (secondWorkerItemLineBuffer.HasValue())
            {
                _lineOrganizer.Organize(originalLine, secondWorkerItemLineBuffer.ItemBuffer, true);
                ReparentItemContainers(originalLine);
            }
            
            _lineOrganizer.Organize(targetLine, workerItemLineBuffer.ItemBuffer, true);
            item.transform.SetParent(targetLine.transform);
            
            return true;
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

        /// <summary>
        /// If second worker is null, no attempt on swap will be done
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <param name="original"></param>
        /// <param name="targetLine"></param>
        /// <param name="targetLineBuffer"></param>
        /// <param name="item"></param>
        /// <param name="workerItemLineBuffer"></param>
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

        // ToDo: it is heavy-handed, but save some time for now 
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