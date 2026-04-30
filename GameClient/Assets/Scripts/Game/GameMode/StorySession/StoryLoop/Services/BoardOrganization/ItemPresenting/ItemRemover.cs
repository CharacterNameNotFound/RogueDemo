using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public class ItemRemover : IItemRemover
    {
        private IItemContainersManager _containersManager;

        public ItemRemover(IItemContainersManager containersManager)
        {
            _containersManager = containersManager;
        }

        public void RemoveItem(ItemContainerComponent item)
        {
            Addressables.Release(item.ItemRenderer.sprite);
            _containersManager.Return(item);
        }
    }
}