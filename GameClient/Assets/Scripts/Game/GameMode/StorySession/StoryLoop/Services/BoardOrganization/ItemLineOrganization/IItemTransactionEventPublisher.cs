using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.Utilities.EventArguments;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public interface IItemTransactionEventPublisher
    {
        public UniTask HandlePreItemMovement(PreItemMovementArguments arguments, CancellationToken cancellationToken);
        public UniTask HandlePostItemMovement(ItemContainerComponent item, CancellationToken cancellationToken);
        
        public UniTask HandlePreItemSell(ItemContainerComponent item, CancellationToken cancellationToken);
        public UniTask HandlePostItemSell(Item item, CancellationToken cancellationToken);
        
        public UniTask HandlePreItemPurchase(ItemContainerComponent item, CancellationToken cancellationToken);
        public UniTask HandlePostItemPurchase(ItemContainerComponent item, CancellationToken cancellationToken);
        
        public UniTask HandlePreItemUpgrade(ItemContainerComponent item, CancellationToken cancellationToken);
        public UniTask HandlePostItemUpgrade(ItemContainerComponent item, CancellationToken cancellationToken);
    }
}