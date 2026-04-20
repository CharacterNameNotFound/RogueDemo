using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    /// <summary>
    /// Interface for item and item line manipulation, that requires no dependencies (like reference to game field)
    /// </summary>
    public interface IItemLineOrganizer
    {
        public void Initialize();
        public void CleanUp();
        public bool TryBuildItemConfiguration(ItemLineComponent originalItemLine, ItemContainerComponent item, ref int index, ref ItemContainerComponent[] itemConfiguration);
        public bool TryBuildItemConfiguration(ItemContainerComponent[] originalItemLine, ItemContainerComponent item, ref int index, ref ItemContainerComponent[] itemConfiguration);
        public void Organize(Bounds itemLineBounds, ItemContainerComponent[] itemConfiguration);
        public void Organize(ItemLineComponent itemLine, ItemContainerComponent[] itemConfiguration, bool writeIntoItemLine);
        public bool TryGetLineIndexForPosition(ItemLineComponent itemLineComponent, Vector3 position, out int index);
        public bool IsLocatedInItemLine(ItemLineComponent itemLineComponent, ItemContainerComponent itemComponent);
        public bool IsLocatedInItemLine(ItemLineComponent itemLineComponent, Vector3 position);
        public bool RemoveItem(ItemLineComponent itemLineComponent, ItemContainerComponent itemComponent);
        public bool RemoveItem(ItemContainerComponent[] itemLine, ItemContainerComponent itemComponent);
    }
}