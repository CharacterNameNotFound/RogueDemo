using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Battle;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Utils.UtilityTypes.Result;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying
{
    public class EncounterPlayer : IEncounterPlayer
    {
        private IEncounterRegistry _encounterRegistry;
        private IBattleEncounterPlayer _battleEncounterPlayer;


        private Encounter _currentEncounter;
            
        
        public EncounterPlayer(IEncounterRegistry encounterRegistry, IBattleEncounterPlayer battleEncounterPlayer)
        {
            _encounterRegistry = encounterRegistry;
            _battleEncounterPlayer = battleEncounterPlayer;
        }

        // for now just in case resetting state
        public void Initialize()
        {
            _currentEncounter = null;
        }

        public bool CanMoveItem(ItemContainerComponent itemContainer)
        {
            if (_currentEncounter is null)
            {
                return true;
            }
            
            return _currentEncounter.CanMoveItem(itemContainer);
        }

        public UniTask HandlePreItemMove(ItemContainerComponent itemContainer, CancellationToken cancellationToken)
        {
            if (_currentEncounter is null)
            {
                return UniTask.CompletedTask;
            }
            
            return _currentEncounter.PreItemMove(cancellationToken);
        }

        public async UniTask PlayEncounter(string encounterId, IStoryContext storyContext, CancellationToken cancellationToken)
        {
            RequestResult<Encounter> requestResult = await _encounterRegistry.GetOrLoadById(encounterId, cancellationToken);

            if (requestResult.IsFailure())
            {
                throw requestResult.Exception;
            }

            Encounter encounter = requestResult.GetValue();

            _currentEncounter = encounter;
            ProjectContext.Instance.Container.Inject(_currentEncounter);

            await _currentEncounter.Play(storyContext, cancellationToken);

            _currentEncounter = null;
        }
        
        
    }
}