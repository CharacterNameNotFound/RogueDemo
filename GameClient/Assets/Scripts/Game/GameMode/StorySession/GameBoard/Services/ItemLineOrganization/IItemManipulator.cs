using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    /// <summary>
    /// Interface for item and item line manipulation, that requires dependencies (like reference to game field)
    /// </summary>
    public interface IItemManipulator
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public bool TryGetItemLineForItem(ItemContainerComponent item, out ItemLineComponent line);
        public void CleanUp();
    }
}