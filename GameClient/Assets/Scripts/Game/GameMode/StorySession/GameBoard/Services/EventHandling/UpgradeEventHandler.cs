using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.BoardModelManipulation;
using Game.GameMode.StorySession.Utilities.EventArguments;
using Utils.UtilityTypes.EventProcessing;

namespace Game.GameMode.StorySession.GameBoard.Services.EventHandling
{
    public class UpgradeEventHandler : IUpgradeEventHandler
    {
        private IBoardModelManipulator _boardModelManipulator;


        public UpgradeEventHandler(
            IEventDispatcher<PreItemUpgradeArgument> preItemUpgrade, 
            IEventDispatcher<PostItemUpgradeArguments> postItemUpgrade, 
            IBoardModelManipulator boardModelManipulator)
        {
            _boardModelManipulator = boardModelManipulator;
            
            preItemUpgrade.RegisterHandler(HandlePreUpgrade);
            postItemUpgrade.RegisterHandler(HandlePostUpgrade);
        }
        
        private UniTask HandlePreUpgrade(PreItemUpgradeArgument preItemMovementArguments, CancellationToken cancellationToken)
        {
            
            
            return UniTask.CompletedTask;
        }

        private UniTask HandlePostUpgrade(PostItemUpgradeArguments postItemMovementArguments, CancellationToken cancellationToken)
        {
            _boardModelManipulator.UpdatePlayerLines();

            return UniTask.CompletedTask;
        }
        
        
    }
}