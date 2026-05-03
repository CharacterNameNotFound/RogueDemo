using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Utilities.EventArguments;
using Utils.UtilityTypes.EventProcessing;

namespace Game.GameMode.StorySession.GameBoard.Services.EventHandling
{
    public class SellEventHandler : ISellEventHandler
    {


        public SellEventHandler(
            IEventDispatcher<PreItemSellArguments> preItemSell, 
            IEventDispatcher<PostItemSellArguments> postItemSell)
        {
            preItemSell.RegisterHandler(HandlePreSell);
            postItemSell.RegisterHandler(HandlePostSell);
        }
        
        private UniTask HandlePreSell(PreItemSellArguments preItemMovementArguments, CancellationToken cancellationToken)
        {
            
            
            return UniTask.CompletedTask;
        }

        private UniTask HandlePostSell(PostItemSellArguments postItemMovementArguments, CancellationToken cancellationToken)
        {
            

            return UniTask.CompletedTask;
        }
        
        
    }
}