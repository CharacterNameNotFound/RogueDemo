using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying
{
    public interface IEncounterPlayer
    {
        public void Initialize();
        public bool CanMoveItem(ItemContainerComponent itemContainer);
        
        /// <summary>
        /// For cases when something needs to be done before item movement finalized, for example processing payment, or finalizing an event.
        /// </summary>
        public UniTask HandlePreItemMove(ItemContainerComponent itemContainer, CancellationToken cancellationToken);
        
        public UniTask PlayEncounter(string encounterId, IStoryContext storyContext, CancellationToken cancellationToken);
    }
}