using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public class ItemRemover : IItemRemover
    {
        private IItemContainersManager _containersManager;
        private ItemDeckOrganizer _deckOrganizer;

        public ItemRemover(IItemContainersManager containersManager, ItemDeckOrganizer deckOrganizer)
        {
            _containersManager = containersManager;
            _deckOrganizer = deckOrganizer;
        }

        public void RemoveItem(ItemContainerComponent item, bool isForceDoNotReturnItem = false)
        {
            Addressables.Release(item.ItemRenderer.sprite);

            if (!item.StoredItem.IsNonDeck && !isForceDoNotReturnItem)
            {
                _deckOrganizer.Return(item.StoredItem.ItemRarity, item.StoredItem.ItemId);
            }
            
            _containersManager.Return(item);
        }
    }
}