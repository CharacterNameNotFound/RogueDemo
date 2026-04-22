using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemContainers
{
    public interface IItemContainersManager
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public UniTask<ItemContainerComponent> GetBySize(int itemItemSize, CancellationToken cancellationToken);
        public void Return(ItemContainerComponent itemContainerComponent);
        public void CleanUp();
    }
}