using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemContainers
{
    public interface IItemContainersManager
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public void CleanUp();

    }
}