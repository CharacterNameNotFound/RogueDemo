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


        public Encounter CurrentEncounter { get; private set; }


        public EncounterPlayer(IEncounterRegistry encounterRegistry, IBattleEncounterPlayer battleEncounterPlayer)
        {
            _encounterRegistry = encounterRegistry;
            _battleEncounterPlayer = battleEncounterPlayer;
        }

        // for now just in case resetting state
        public void Initialize()
        {
            CurrentEncounter = null;
        }

        public bool CanMoveItem(ItemContainerComponent itemContainer, ItemLineComponent originalItemLine)
        {
            if (CurrentEncounter is null)
            {
                return true;
            }
            
            return CurrentEncounter.CanMoveItem(itemContainer);
        }

        public bool CanSellItem(ItemContainerComponent itemContainer)
        {
            if (CurrentEncounter is null)
            {
                return true;
            }
            
            return CurrentEncounter.CanSellItem(itemContainer);
        }

        public UniTask HandlePreItemMove(ItemContainerComponent itemContainer, CancellationToken cancellationToken)
        {
            if (CurrentEncounter is null)
            {
                return UniTask.CompletedTask;
            }
            
            return CurrentEncounter.PreItemMove(cancellationToken);
        }

        public async UniTask PlayEncounter(string encounterId, IStoryContext storyContext, CancellationToken cancellationToken)
        {
            RequestResult<Encounter> requestResult = await _encounterRegistry.GetOrLoadById(encounterId, cancellationToken);

            if (requestResult.IsFailure())
            {
                throw requestResult.Exception;
            }

            Encounter encounter = requestResult.GetValue();

            CurrentEncounter = encounter;
            ProjectContext.Instance.Container.Inject(CurrentEncounter);

            await CurrentEncounter.Play(storyContext, cancellationToken);

            CurrentEncounter = null;
        }
        
        
    }
}