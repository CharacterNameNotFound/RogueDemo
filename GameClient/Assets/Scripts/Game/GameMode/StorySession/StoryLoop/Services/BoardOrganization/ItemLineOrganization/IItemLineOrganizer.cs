using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    /// <summary>
    /// Interface for item and item line manipulation, that requires no dependencies (like reference to game field)
    /// </summary>
    public interface IItemLineOrganizer
    {
        public bool TryBuildItemConfiguration(ItemLineComponent originalItemLine, ItemContainerComponent item, ref int index, ItemContainerComponent[] result);
        public bool TryBuildItemConfiguration(ItemContainerComponent[] originalItemLine, ItemContainerComponent item, ref int index, ItemContainerComponent[] result);

        public bool TryMakeSwap(
            Vector3 position,
            int targetItemOriginalIndex,
            ItemLineComponent originalLine,
            ItemLineComponent targetLine, 
            ItemContainerComponent targetItem,
            ItemContainerComponent[] originalLineResult, 
            ItemContainerComponent[] targetLineResult);
        
        public void Organize(Bounds itemLineBounds, ItemContainerComponent[] itemConfiguration);
        public void Organize(ItemLineComponent itemLine, ItemContainerComponent[] itemConfiguration, bool writeIntoItemLine);
        public bool TryGetLineIndexForPosition(ItemLineComponent itemLineComponent, Vector3 position, out int index);
        public bool IsLocatedInItemLine(ItemLineComponent itemLineComponent, ItemContainerComponent itemComponent);
        public bool IsLocatedInItemLine(ItemLineComponent itemLineComponent, Vector3 position);
        public bool RemoveItem(ItemLineComponent itemLineComponent, ItemContainerComponent itemComponent, out int targetItemOriginalIndex);
        public bool RemoveItem(ItemContainerComponent[] itemLine, ItemContainerComponent itemComponent, out int targetItemOriginalIndex);
    }
}