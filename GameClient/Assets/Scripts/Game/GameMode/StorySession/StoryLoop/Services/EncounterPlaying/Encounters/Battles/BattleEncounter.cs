using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.Utilities.WorldInteractables.Awaiters;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using IBattleEncounterRoutines = Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles.Routines.IBattleEncounterRoutines;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles
{
    public class BattleEncounter : Encounter
    {
        [field: Header("Battle encounter configs")]
        [field: SerializeField] public AssetReferenceSprite VerticalPortrait { get; protected set; }
        
        [field: Space(10)]
        [field: SerializeField] public int CoinReward { get; set; }
        [field: SerializeField] public int ExperienceReward { get; set; }
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public List<string> Items { get; set; } = new(12);
        
        public override EncounterType EncounterType => EncounterType.Battle;
        
        
        private GameBoardHolder _gameBoardHolder;
        private IBattleEncounterRoutines _battleEncounterRoutines;
        private IStoryContextProvider _storyContextProvider;
        private IItemStatGetter _itemStatGetter;
        private IPlayerStatusUpdater _statusUpdater;
        private IGameBoardModelHolder _gameBoardModelHolder;
        
        
        [Inject]
        private void Construct(
            GameBoardHolder gameBoardHolder, 
            IBattleEncounterRoutines battleEncounterRoutines, 
            IStoryContextProvider storyContextProvider,
            IItemStatGetter itemStatGetter,
            IPlayerStatusUpdater statusUpdater,
            IGameBoardModelHolder gameBoardModelHolder)
        {
            _gameBoardHolder = gameBoardHolder;
            _battleEncounterRoutines = battleEncounterRoutines;
            _storyContextProvider = storyContextProvider;
            _itemStatGetter = itemStatGetter;
            _statusUpdater = statusUpdater;
            _gameBoardModelHolder = gameBoardModelHolder;
        }
        
        public override async UniTask Play(GameBoardModel gameBoardModel, CancellationToken cancellationToken)
        {
            await _battleEncounterRoutines.ShowElements(this, cancellationToken);
            await _battleEncounterRoutines.LoadItemsUpdateViews(Items, cancellationToken);
            
            await InteractablePressWaiter.WaitForPress(
                _gameBoardHolder.GameBoardComponent.GameBoardInteractables.BattleScreenButton, 
                cancellationToken);
            

            await _battleEncounterRoutines.HideAll(cancellationToken);
        }
        
    }
}