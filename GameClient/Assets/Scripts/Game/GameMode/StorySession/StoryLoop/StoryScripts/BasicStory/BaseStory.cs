using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Services.TextsDrawing;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.PlayerStashEncounter;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection;
using Game.GameMode.StorySession.StoryLoop.Services.InputControl;
using Game.GameMode.StorySession.StoryLoop.Services.StoryFinalization;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services.GameSaving;
using Game.GameMode.StorySession.UI;
using Game.GameMode.StorySession.Utilities.WorldInteractables;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.InputManager;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class BaseStory : ScriptableObject, IStoryBase
    {
        private BuildAndRegisterDecksRoutine _buildAndRegisterDecksRoutine;
        private ILoadingScreenManager _loadingScreenManager;
        private BaseStoryConfigs _baseStoryConfigs;
        private UIManager _uiManager;
        private StorySessionScreenBuilder _storySessionScreenBuilder;
        private GameBoardInitializationRoutine _boardInitializationRoutine;
        private BaseStoryBossSelector _baseStoryBossSelector;
        private BaseStorySaveManager _baseStorySaveManager;
        private BaseStoryCycleGenerator _baseStoryCycleGenerator;
        private IStoryFinalizer _storyFinalizer;
        private IEncounterSelector _encounterSelector;
        private IEncounterPlayer _encounterPlayer;
        private IItemContainersManager _containersManager;
        private IItemPresenter _itemPresenter;
        private IPlayerStashController _playerStashController;
        private IStoryContextProvider _storyContextProvider;
        private IInputLayerControlMediator _inputLayerControlMediator;
        private ISessionStatusDrawer _sessionStatusDrawer;
        private IGameBoardModelCreator _boardModelCreator;
        private IGameBoardModelHolder _gameBoardModelHolder;
        private IHeroesHpDrawer _heroHpDrawer;
        
        private BaseStoryContext _baseStoryContext;

        // ToDo: initialization will need to sliced somewhat
        [Inject]
        private void InjectDependencies(
            ILoadingScreenManager loadingScreenManager,
            BaseStoryConfigs baseStoryConfigs,
            BuildAndRegisterDecksRoutine buildAndRegisterDecksRoutine,
            UIManager uiManager,
            StorySessionScreenBuilder storySessionScreenBuilder,
            GameBoardInitializationRoutine boardInitializationRoutine,
            BaseStoryBossSelector baseStoryBossSelector,
            BaseStorySaveManager baseStorySaveManager,
            BaseStoryCycleGenerator baseStoryCycleGenerator,
            IStoryFinalizer storyFinalizer,
            IEncounterSelector encounterSelector,
            IEncounterPlayer encounterPlayer,
            IItemContainersManager containersManager,
            IItemPresenter itemPresenter,
            IPlayerStashController playerStashController,
            IStoryContextProvider storyContextProvider,
            IInputLayerControlMediator inputLayerControlMediator,
            ISessionStatusDrawer sessionStatusDrawer,
            IGameBoardModelCreator boardModelCreator,
            IGameBoardModelHolder gameBoardModelHolder,
            IHeroesHpDrawer heroHpDrawer
            )
        {
            _loadingScreenManager = loadingScreenManager;
            _baseStoryConfigs = baseStoryConfigs;
            _buildAndRegisterDecksRoutine = buildAndRegisterDecksRoutine;
            _uiManager = uiManager;
            _storySessionScreenBuilder = storySessionScreenBuilder;
            _boardInitializationRoutine = boardInitializationRoutine;
            _baseStoryBossSelector = baseStoryBossSelector;
            _baseStorySaveManager = baseStorySaveManager;
            _baseStoryCycleGenerator = baseStoryCycleGenerator;
            _storyFinalizer = storyFinalizer;
            _encounterSelector = encounterSelector;
            _encounterPlayer = encounterPlayer;
            _containersManager = containersManager;
            _itemPresenter = itemPresenter;
            _playerStashController = playerStashController;
            _storyContextProvider = storyContextProvider;
            _inputLayerControlMediator = inputLayerControlMediator;
            _sessionStatusDrawer = sessionStatusDrawer;
            _boardModelCreator = boardModelCreator;
            _gameBoardModelHolder = gameBoardModelHolder;
            _heroHpDrawer = heroHpDrawer;
        }

        public async UniTask Initialize(StoryInitializationData storyInitializationData, CancellationToken cancellationToken)
        {
            _baseStoryContext = new BaseStoryContext();
            
            _storyContextProvider.Set(_baseStoryContext);

            await (storyInitializationData.TryReadSaveFile ? ReadSaveFile(cancellationToken) : GenerateSessionData(storyInitializationData, cancellationToken));
            
        }

        public async UniTask StartStory(CancellationToken cancellationToken)
        {
            await _uiManager.OpenScreenRequest(_storySessionScreenBuilder, null, out ScreenHolder screen).Play(cancellationToken);

            StorySessionScreenController screenController = (StorySessionScreenController) screen.ScreenBase;
            await screenController.SetBossImages(_baseStoryContext.Bosses, cancellationToken);

            // removing loading screen
            await _loadingScreenManager.Hide(true, cancellationToken);
            //////////////////////////


            // playing bosses animation
            await screenController.PlayBossIntro(cancellationToken);
            
            // starting game loop from game mode
        }
        

        public async UniTask Loop(CancellationToken cancellationToken)
        {
            // save each cycle
            _sessionStatusDrawer.RedrawPlayerStats(_gameBoardModelHolder.GameBoardModel);
            
            do
            {
                GameBoardModel gameBoardModel = _gameBoardModelHolder.GameBoardModel;

                _sessionStatusDrawer.RedrawStoryProgression(gameBoardModel);
                _inputLayerControlMediator.ToggleItemMovement(true);
                int turn = gameBoardModel.StoryStats.Cycle * _baseStoryConfigs.StoryDayLength + gameBoardModel.StoryStats.Step;
                int selectedEncounterIndex = await _encounterSelector.StartEncounterSelection(_baseStoryContext.StoryEncounters[turn], cancellationToken);
                string encounterId = _baseStoryContext.StoryEncounters[turn][selectedEncounterIndex];

                await _encounterPlayer.PlayEncounter(encounterId, gameBoardModel, cancellationToken);

                // updating turn counters
                gameBoardModel.StoryStats.Step++;

                if (gameBoardModel.StoryStats.Step == _baseStoryConfigs.StoryDayLength)
                {
                    gameBoardModel.StoryStats.Step = 0;
                    gameBoardModel.StoryStats.Cycle++;
                    
                    _inputLayerControlMediator.ToggleItemMovement(false);
                    await _baseStorySaveManager.Save(_baseStoryContext, cancellationToken);
                    _inputLayerControlMediator.ToggleItemMovement(true);
                    
                    _baseStoryCycleGenerator.AppendDay(_baseStoryContext, _baseStoryConfigs, _gameBoardModelHolder.GameBoardModel);
                }
                

            } while (!_storyFinalizer.IsFinalizationRequested());
            
            // remove 6 last steps from history
            _inputLayerControlMediator.ToggleItemMovement(false);
        }

        public UniTask Finish(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public UniTask CleanUp(CancellationToken cancellationToken)
        {
            Addressables.Release(_baseStoryContext.CharacterData);
            
            return UniTask.CompletedTask;
        }
        
        
        private async UniTask GenerateSessionData(
            StoryInitializationData storyInitializationData,
            CancellationToken cancellationToken)
        {
            _baseStoryContext.CharacterData = await storyInitializationData.CharacterId.Load<CharacterData>(cancellationToken);
            
            GameBoardModel gameBoardModel = _boardModelCreator.CrateNew(_baseStoryConfigs.GameBoardModelCreationConfigs, _baseStoryContext.CharacterData);
            _gameBoardModelHolder.Set(gameBoardModel);
            
            // generate and save curses
            // Empty for now
            
            
            // generate encounters decks
            await _buildAndRegisterDecksRoutine.BuildAndRegisterEncounters(
                _baseStoryContext.CharacterData.EncounterSets, 
                _baseStoryConfigs.EncounterSets, 
                _baseStoryConfigs, 
                cancellationToken);
            
            
            // generate item decks
            await _buildAndRegisterDecksRoutine.BuildAndRegistriesItems(
                _baseStoryConfigs.NeutralItemSets, 
                _baseStoryContext.CharacterData.ItemSets, 
                _baseStoryConfigs, 
                _baseStoryConfigs, 
                cancellationToken);
            

            // generate bosses
            await _baseStoryBossSelector.SelectBosses(_baseStoryConfigs, _baseStoryContext, cancellationToken);

            // build first cycle
            _baseStoryCycleGenerator.GenerateFirstDayEntry(_baseStoryContext, _baseStoryConfigs);
            
            
            
            // Unity managed systems
            await InitializeUnityManagedSystems(cancellationToken);
            
            
            // save decks, events, bosses
            await _baseStorySaveManager.Save(_baseStoryContext, cancellationToken);
        }

        private async UniTask ReadSaveFile(CancellationToken cancellationToken)
        {
            await _baseStorySaveManager.Load(_baseStoryContext, _baseStoryConfigs, cancellationToken);
            
            // Unity managed systems
            await InitializeUnityManagedSystems(cancellationToken);
            
        }

        private async UniTask InitializeUnityManagedSystems(CancellationToken cancellationToken)
        {
            // Loading game board
            await _boardInitializationRoutine.Initialize(_baseStoryContext.CharacterData, cancellationToken);
            
            // generate object pools
            await _containersManager.Initialize(cancellationToken);
            await _itemPresenter.Initialize(cancellationToken);
            
            // generating encounter selection related
            await _encounterSelector.Initialize(cancellationToken);

            // Input related
            await _inputLayerControlMediator.Initialize(cancellationToken);
            await _playerStashController.Initialize(cancellationToken);
            
            // resetting systems
            _encounterPlayer.Initialize();
            _sessionStatusDrawer.Initialize(_baseStoryConfigs.StoryDayLength);
            
            // view
            
            _heroHpDrawer.UpdateHeroHpBar(HeroGroup.Player);
        }
        
        
    }
}