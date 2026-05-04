using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.StoryLoop.Services.InputControl;
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
        private IInputLayerControlMediator _inputLayerControlMediator;
        private ISimulationPlayer _simulationPlayer;
        
        
        [Inject]
        private void Construct(
            GameBoardHolder gameBoardHolder, 
            IBattleEncounterRoutines battleEncounterRoutines,
            IInputLayerControlMediator inputLayerControlMediator,
            ISimulationPlayer simulationPlayer)
        {
            _gameBoardHolder = gameBoardHolder;
            _battleEncounterRoutines = battleEncounterRoutines;
            _inputLayerControlMediator = inputLayerControlMediator;
            _simulationPlayer = simulationPlayer;
        }
        
        public override async UniTask Play(GameBoardModel gameBoardModel, CancellationToken cancellationToken)
        {
            _inputLayerControlMediator.ToggleBattle(true);
            await _battleEncounterRoutines.ShowElements(this, cancellationToken);
            await _battleEncounterRoutines.LoadItemsUpdateViews(Items, cancellationToken);

            await _simulationPlayer.PlaySimulation(cancellationToken);
            
            await InteractablePressWaiter.WaitForPress(
                _gameBoardHolder.GameBoardComponent.GameBoardInteractables.BattleScreenButton, 
                cancellationToken);
            
            _inputLayerControlMediator.ToggleBattle(false);

            await _battleEncounterRoutines.HideAll(cancellationToken);
        }
        
    }
}