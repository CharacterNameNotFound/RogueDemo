using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.Utilities.EventArguments;
using Utils.UtilityTypes.EventProcessing;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public class ItemTransactionEventPublisher : IItemTransactionEventPublisher
    {
        private IEventDispatcher<PreItemSellArguments> _preItemSell;
        private IEventDispatcher<PostItemSellArguments> _postItemSell;
        
        private IEventDispatcher<PreItemMovementArguments> _preItemMovement;
        private IEventDispatcher<PostItemMovementArguments> _postItemMovement;
        
        private IEventDispatcher<PreItemUpgradeArgument> _preItemUpgrade;
        private IEventDispatcher<PostItemUpgradeArguments> _postItemUpgrade;

        private IEventDispatcher<PreItemPurchaseArguments> _preItemPurchase;
        private IEventDispatcher<PostItemPurchaseArguments> _postItemPurchase;

        public ItemTransactionEventPublisher(
            IEventDispatcher<PreItemSellArguments> preItemSell, 
            IEventDispatcher<PostItemSellArguments> postItemSell, 
            IEventDispatcher<PreItemMovementArguments> preItemMovement, 
            IEventDispatcher<PostItemMovementArguments> postItemMovement, 
            IEventDispatcher<PreItemUpgradeArgument> preItemUpgrade, 
            IEventDispatcher<PostItemUpgradeArguments> postItemUpgrade, 
            IEventDispatcher<PreItemPurchaseArguments> preItemPurchase, 
            IEventDispatcher<PostItemPurchaseArguments> postItemPurchase)
        {
            _preItemSell = preItemSell;
            _postItemSell = postItemSell;
            _preItemMovement = preItemMovement;
            _postItemMovement = postItemMovement;
            _preItemUpgrade = preItemUpgrade;
            _postItemUpgrade = postItemUpgrade;
            _preItemPurchase = preItemPurchase;
            _postItemPurchase = postItemPurchase;
        }
        
        
        public UniTask HandlePreItemMovement(PreItemMovementArguments arguments, CancellationToken cancellationToken)
        {
            return _preItemMovement.Invoke(arguments, cancellationToken);
        }

        public UniTask HandlePostItemMovement(ItemContainerComponent item, CancellationToken cancellationToken)
        {
            PostItemMovementArguments arg = new PostItemMovementArguments(item);
            return _postItemMovement.Invoke(arg, cancellationToken);
        }

        public UniTask HandlePreItemSell(ItemContainerComponent item, CancellationToken cancellationToken)
        {
            PreItemSellArguments preItemSellArguments = new PreItemSellArguments(item);
            return _preItemSell.Invoke(preItemSellArguments, cancellationToken);
        }

        public UniTask HandlePostItemSell(Item item, CancellationToken cancellationToken)
        {
            PostItemSellArguments postItemSellArguments = new PostItemSellArguments();
            return _postItemSell.Invoke(postItemSellArguments, cancellationToken);
        }

        public UniTask HandlePreItemPurchase(ItemContainerComponent item, CancellationToken cancellationToken)
        {
            return _preItemPurchase.Invoke(new PreItemPurchaseArguments(item), cancellationToken);
        }

        public UniTask HandlePostItemPurchase(ItemContainerComponent item, CancellationToken cancellationToken)
        {
            return _postItemPurchase.Invoke(new PostItemPurchaseArguments(), cancellationToken);
        }

        public UniTask HandlePreItemUpgrade(ItemContainerComponent item, CancellationToken cancellationToken)
        {
            return _preItemUpgrade.Invoke(new PreItemUpgradeArgument(), cancellationToken);
        }

        public UniTask HandlePostItemUpgrade(ItemContainerComponent item, CancellationToken cancellationToken)
        {
            return _postItemUpgrade.Invoke(new PostItemUpgradeArguments(), cancellationToken);
        }
        
        
        
        
    }
}