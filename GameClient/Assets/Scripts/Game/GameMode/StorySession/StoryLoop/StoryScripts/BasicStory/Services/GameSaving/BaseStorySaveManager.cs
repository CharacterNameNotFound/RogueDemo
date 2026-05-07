using System.IO;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Services.TextsDrawing;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectDisplaying;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects;
using Game.GameMode.StorySession.GameBoard.View.Utils;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.PlayerStashEncounter;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection;
using Game.GameMode.StorySession.StoryLoop.Services.InputControl;
using Game.GameMode.StorySession.StoryLoop.Services.ItemLineSaveLoad;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines;
using Game.Session;
using GameWideSystems.RNGManagement;
using GameWideSystems.SessionManagement.Sessions;
using Newtonsoft.Json;
using Utils.DiscInteraction;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services.GameSaving
{
    public class BaseStorySaveManager
    {
        private GenericPathProvider _genericPathProvider;
        private SessionHolder _sessionHolder;
        private JsonSerializerSettings _jsonSerializerSettings;
        private ItemDeckOrganizer _itemDeckOrganizer;
        private EncounterDeckOrganizer _encounterDeckOrganizer;
        private IRNGManager _rngManager;
        private IEncounterLoader _encounterLoader;
        private IEncounterRegistry _encounterRegistry;
        private IItemRegistry _itemRegistry;
        private IGameBoardModelHolder _gameBoardModelHolder;
        private IGameBoardModelCreator _gameBoardModelCreator;
        private IItemLineLoader _itemLineLoader;
        private IItemLineSaver _itemLineSaver;
        private GameBoardInitializationRoutine _boardInitializationRoutine;
        private IItemContainersManager _containersManager;
        private IItemPresenter _itemPresenter;
        private IEncounterSelector _encounterSelector;
        private IInputLayerControlMediator _inputLayerControlMediator;
        private IPlayerStashController _playerStashController;
        private IEncounterPlayer _encounterPlayer;
        private ISessionStatusDrawer _sessionStatusDrawer;
        private IHeroesHpDrawer _heroHpDrawer;
        private IStoryVisualEffectManager _storyVisualEffectManager;
        private IHeroStatusDisplayManager _heroStatusDisplayManager;
        private GameBoardHolder _gameBoardHolder;

        
        
        public BaseStorySaveManager(
            JsonSerializerSettings jsonSerializerSettings, 
            GenericPathProvider genericPathProvider, 
            SessionHolder sessionHolder, 
            ItemDeckOrganizer itemDeckOrganizer, 
            EncounterDeckOrganizer encounterDeckOrganizer, 
            IRNGManager rngManager, 
            IEncounterLoader encounterLoader, 
            IEncounterRegistry encounterRegistry, 
            IItemRegistry itemRegistry, 
            IGameBoardModelHolder gameBoardModelHolder, 
            IGameBoardModelCreator gameBoardModelCreator, 
            IItemLineLoader itemLineLoader, 
            IItemLineSaver itemLineSaver, 
            GameBoardInitializationRoutine boardInitializationRoutine, 
            IItemContainersManager containersManager, 
            IItemPresenter itemPresenter, 
            IEncounterSelector encounterSelector, 
            IInputLayerControlMediator inputLayerControlMediator, 
            ISessionStatusDrawer sessionStatusDrawer, 
            IEncounterPlayer encounterPlayer, 
            IPlayerStashController playerStashController, 
            IHeroesHpDrawer heroHpDrawer, 
            IStoryVisualEffectManager storyVisualEffectManager, 
            IHeroStatusDisplayManager heroStatusDisplayManager, 
            GameBoardHolder gameBoardHolder)
        {
            _jsonSerializerSettings = jsonSerializerSettings;
            _genericPathProvider = genericPathProvider;
            _sessionHolder = sessionHolder;
            _itemDeckOrganizer = itemDeckOrganizer;
            _encounterDeckOrganizer = encounterDeckOrganizer;
            _rngManager = rngManager;
            _encounterLoader = encounterLoader;
            _encounterRegistry = encounterRegistry;
            _itemRegistry = itemRegistry;
            _gameBoardModelHolder = gameBoardModelHolder;
            _gameBoardModelCreator = gameBoardModelCreator;
            _itemLineLoader = itemLineLoader;
            _itemLineSaver = itemLineSaver;
            _boardInitializationRoutine = boardInitializationRoutine;
            _containersManager = containersManager;
            _itemPresenter = itemPresenter;
            _encounterSelector = encounterSelector;
            _inputLayerControlMediator = inputLayerControlMediator;
            _sessionStatusDrawer = sessionStatusDrawer;
            _encounterPlayer = encounterPlayer;
            _playerStashController = playerStashController;
            _heroHpDrawer = heroHpDrawer;
            _storyVisualEffectManager = storyVisualEffectManager;
            _heroStatusDisplayManager = heroStatusDisplayManager;
            _gameBoardHolder = gameBoardHolder;
        }
        

        public async UniTask Save(BaseStoryContext baseStoryContext, CancellationToken cancellationToken)
        {
            BaseStorySaveFile saveFile = new BaseStorySaveFile();
            
            // base story context
            saveFile.StoryEncounters = baseStoryContext.StoryEncounters;
            saveFile.Bosses = baseStoryContext.Bosses.Select(item => item.EncounterId).ToArray();
            saveFile.CharacterId = baseStoryContext.CharacterData.CharacterId;
            saveFile.StoryStats = _gameBoardModelHolder.GameBoardModel.StoryStats;
            
            
            // encounters
            saveFile.EncounterDeckOrganizerState = _encounterDeckOrganizer.GetState(_jsonSerializerSettings);
            saveFile.EncounterRegistryIds = _encounterRegistry.GetAllRegisteredIds();
            
            // items
            saveFile.ItemDeckOrganizerState = _itemDeckOrganizer.GetState(_jsonSerializerSettings);
            saveFile.ItemRegistryIds = _itemRegistry.GetAllRegisteredIds();
            
            
            // Player
            ItemLineSaveData itemLineSaveData = _itemLineSaver.GetSaveData();
            saveFile.PlayerItemsData = itemLineSaveData;
            saveFile.StoryStats = _gameBoardModelHolder.GameBoardModel.StoryStats;
            saveFile.PlayerHeroStats = _gameBoardModelHolder.GameBoardModel.PlayerHeroStats;
            saveFile.PlayerStats = _gameBoardModelHolder.GameBoardModel.PlayerStats;
            

            string path = _genericPathProvider.InProfileSavesPath(_sessionHolder.Session.InternalId);
            path = Path.Combine(path, nameof(BaseStorySaveFile));
            ProcedureResult result = await DiscWriting.ConvertAndWriteJson(saveFile, path, _jsonSerializerSettings, cancellationToken);
            if (result.IsFailure())
            {
                throw result.Exception;
            }
        }

        public async UniTask Load(BaseStoryContext baseStoryContext, BaseStoryConfigs baseStoryConfigs, CancellationToken cancellationToken)
        {
            string path = _genericPathProvider.InProfileSavesPath(_sessionHolder.Session.InternalId);
            path = Path.Combine(path, nameof(BaseStorySaveFile));
            RequestResult<BaseStorySaveFile> result = await DiscReading.ReadAndConvertJson<BaseStorySaveFile>(path, _jsonSerializerSettings, cancellationToken);

            if (result.IsFailure())
            {
                throw result.Exception;
            }

            BaseStorySaveFile saveFile = result.GetValue();

            // base story context
            baseStoryContext.CharacterData = await saveFile.CharacterId.Load<CharacterData>(cancellationToken);
            
            // Loading game board
            await _boardInitializationRoutine.Initialize(baseStoryContext.CharacterData, cancellationToken);
            
            GameBoardModel gameBoardModel = _gameBoardModelCreator.CrateNew(baseStoryConfigs.GameBoardModelCreationConfigs, baseStoryContext.CharacterData);
            _gameBoardModelHolder.Set(gameBoardModel);
            
            baseStoryContext.Bosses = (await saveFile.Bosses.Select(item => _encounterLoader.LoadById(item, cancellationToken)))
                .Select(item => item.GetValue())
                .Cast<BattleEncounter>()
                .ToArray();
            
            baseStoryContext.StoryEncounters = saveFile.StoryEncounters;
            gameBoardModel.StoryStats = saveFile.StoryStats;
            gameBoardModel.PlayerHeroStats = saveFile.PlayerHeroStats;
            gameBoardModel.PlayerStats = saveFile.PlayerStats;
            

            // encounters
            _encounterDeckOrganizer.RestoreState(saveFile.EncounterDeckOrganizerState, _jsonSerializerSettings, _rngManager.GetRandomProvider(RNGGroup.CardShuffler));
            await _encounterRegistry.InitializeWithIds(saveFile.EncounterRegistryIds, cancellationToken);
            
            // items
            _itemDeckOrganizer.RestoreState(saveFile.ItemDeckOrganizerState, _jsonSerializerSettings, _rngManager.GetRandomProvider(RNGGroup.CardShuffler));
            await _itemRegistry.InitializeWithIds(saveFile.ItemRegistryIds, cancellationToken);
            
            
            // generate object pools
            await _containersManager.Initialize(cancellationToken);
            await _itemPresenter.Initialize(cancellationToken);
            
            // generating encounter selection related
            await _encounterSelector.Initialize(cancellationToken);

            
            // player
            await _itemLineLoader.Load(saveFile.PlayerItemsData, cancellationToken);
            
            
            
            // Input related
            await _inputLayerControlMediator.Initialize(cancellationToken);
            await _playerStashController.Initialize(cancellationToken);
            
            // resetting systems
            _encounterPlayer.Initialize();
            _sessionStatusDrawer.Initialize(baseStoryConfigs.StoryDayLength);
            await _heroStatusDisplayManager.Initialize(
                GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Player, _gameBoardHolder.GameBoardComponent), 
                GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Encounter, _gameBoardHolder.GameBoardComponent),  
                cancellationToken);
            
            // view
            _heroHpDrawer.UpdateHeroHpBar(HeroGroup.Player);
            
            // vfx
            await _storyVisualEffectManager.Initialize(cancellationToken);

        }
        
    }
}