using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public interface IItemUpgrader
    {
        public UniTask PlayUpgrade(
            ItemContainerComponent upgradableItem, 
            ItemContainerComponent targetItem,
            ItemLineComponent upgradableItemLine,
            CancellationToken cancellationToken);
        
        public void UpdateStats(Item newItem, Item oldItem);
        
    }
}