using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.BoardModelManipulation;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.Utilities.EventArguments;
using Utils.UtilityTypes.EventProcessing;

namespace Game.GameMode.StorySession.GameBoard.Services.EventHandling
{
    // For now, it is okay to just recalculate all bonuses, to save A LOT of time on item swap processing.
    // It is a little bit pricey on calculations, but should be okay
    public class MoveEventHandler : IMoveEventHandler
    {
        private IBoardModelManipulator _boardModelManipulator;
        
        public MoveEventHandler(
            IEventDispatcher<PreItemMovementArguments> preItemMovement, 
            IEventDispatcher<PostItemMovementArguments> postItemMovement, 
            IBoardModelManipulator boardModelManipulator)
        {
            _boardModelManipulator = boardModelManipulator;

            preItemMovement.RegisterHandler(HandlePreMovement);
            postItemMovement.RegisterHandler(HandlePostMovement);
        }


        private UniTask HandlePreMovement(PreItemMovementArguments preItemMovementArguments, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        private UniTask HandlePostMovement(PostItemMovementArguments postItemMovementArguments, CancellationToken cancellationToken)
        {
            _boardModelManipulator.UpdatePlayerLines();

            return UniTask.CompletedTask;
        }
            
            
    }
}