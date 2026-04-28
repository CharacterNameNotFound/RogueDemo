using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    /// <summary>
    /// Interface for item and item line manipulation, that requires dependencies (like reference to game field)
    /// </summary>
    public interface IItemManipulator
    {
        public bool TryGetItemLineForItem(ItemContainerComponent item, out ItemLineComponent line);
        
        public UniTask<bool> TryCompleteItemTransition(Vector3 worldPosition,
            int targetItemOriginalIndex,
            ItemLineComponent originalLine,
            ItemLineComponent targetLine,
            ItemLineBuffer targetLineBuffer,
            ItemContainerComponent item,
            ItemLineBuffer workerItemLineBuffer,
            ItemLineBuffer secondWorkerItemLineBuffer,
            CancellationToken cancellationToken);
        
        public bool TryUpdateItemLines(
            Vector3 worldPosition, 
            ItemLineComponent original, 
            ItemLineComponent targetLine, 
            ItemLineBuffer targetLineBuffer, 
            ItemContainerComponent item, 
            ItemLineBuffer workerItemLineBuffer);

        UniTask<bool> TrySellItem(Vector3 mouseWorldPoint,
            ItemContainerComponent[] itemLine,
            ItemLineComponent originalItemLine,
            ItemContainerComponent item,
            CancellationToken cancellationToken);
    }
}