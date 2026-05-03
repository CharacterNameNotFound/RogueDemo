using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Utilities.EventArguments;
using Utils.UtilityTypes.EventProcessing;

namespace Game.GameMode.StorySession.GameBoard.Services.EventHandling
{
    public class PurchaseEventHandler : IPurchaseEventHandler
    {


        public PurchaseEventHandler(
            IEventDispatcher<PreItemPurchaseArguments> preItemPurchase, 
            IEventDispatcher<PostItemPurchaseArguments> postItemPurchase)
        {
            preItemPurchase.RegisterHandler(HandlePrePurchase);
            postItemPurchase.RegisterHandler(HandlePostPurchase);
        }
        
        private UniTask HandlePrePurchase(PreItemPurchaseArguments preItemMovementArguments, CancellationToken cancellationToken)
        {
            
            
            return UniTask.CompletedTask;
        }

        private UniTask HandlePostPurchase(PostItemPurchaseArguments postItemMovementArguments, CancellationToken cancellationToken)
        {
            

            return UniTask.CompletedTask;
        }
        
        
    }
}