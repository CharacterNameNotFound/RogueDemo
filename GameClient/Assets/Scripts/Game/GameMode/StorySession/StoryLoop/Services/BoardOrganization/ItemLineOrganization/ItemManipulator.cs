using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public class ItemManipulator : IItemManipulator
    {
        private IItemLineOrganizer _lineOrganizer;
        private GameBoardHolder _gameBoardHolder;

        public ItemManipulator(IItemLineOrganizer lineOrganizer, GameBoardHolder gameBoardHolder)
        {
            _lineOrganizer = lineOrganizer;
            _gameBoardHolder = gameBoardHolder;
        }

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            _lineOrganizer.Initialize();
            return UniTask.CompletedTask;
        }

        public bool TryGetItemLineForItem(ItemContainerComponent item, out ItemLineComponent line)
        {
            foreach (ItemLineComponent itemLine in _gameBoardHolder.GameBoardComponent.ItemLineViewController.EnumerateItemLines())
            {
                if (_lineOrganizer.IsLocatedInItemLine(itemLine, item.transform.position))
                {
                    line = itemLine;
                    return true;
                }
                
            }

            line = null;
            return false;
        }


        public async UniTask<bool> TryCompleteItemTransition(
            Vector3 worldPosition, 
            ItemLineComponent original, 
            ItemLineComponent targetLine, 
            ItemLineBuffer targetLineBuffer, 
            ItemContainerComponent item, 
            ItemLineBuffer workerItemLineBuffer,
            CancellationToken cancellationToken)
        {
            
            
            
            bool canUpdate = TryUpdateItemLines(worldPosition, original, targetLine, targetLineBuffer, item, workerItemLineBuffer);
            
                
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
            // add between lines item swap implementation here
            
            
            if (!_lineOrganizer.TryGetLineIndexForPosition(targetLine, worldPosition, out int index))
            {
                return false;
            }
            
            workerItemLineBuffer.ClearBuffer();
            if (!_lineOrganizer.TryBuildItemConfiguration(targetLineBuffer.ItemBuffer, item, ref index, ref workerItemLineBuffer.ItemBuffer))
            {
                return false;
            }
            
            _lineOrganizer.Organize(targetLine, workerItemLineBuffer.ItemBuffer, true);
            
            return true;
        }
        

        public void CleanUp()
        {
            _lineOrganizer.CleanUp();
        }
    }
}