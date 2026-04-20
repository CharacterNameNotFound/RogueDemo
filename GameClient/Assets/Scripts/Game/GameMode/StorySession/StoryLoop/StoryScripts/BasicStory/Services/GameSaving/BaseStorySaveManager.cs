using System.IO;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
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

        
        public BaseStorySaveManager(
            JsonSerializerSettings jsonSerializerSettings, 
            GenericPathProvider genericPathProvider, 
            SessionHolder sessionHolder, 
            ItemDeckOrganizer itemDeckOrganizer, 
            EncounterDeckOrganizer encounterDeckOrganizer, 
            IRNGManager rngManager, 
            IEncounterLoader encounterLoader, 
            IEncounterRegistry encounterRegistry, 
            IItemRegistry itemRegistry)
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
        }
        

        public async UniTask Save(BaseStoryContext baseStoryContext, CancellationToken cancellationToken)
        {
            BaseStorySaveFile saveFile = new BaseStorySaveFile();
            
            // base story context
            saveFile.StoryEncounters = baseStoryContext.StoryEncounters;
            saveFile.Bosses = baseStoryContext.Bosses.Select(item => item.EncounterId).ToArray();
            saveFile.CharacterId = baseStoryContext.CharacterData.CharacterId;
            saveFile.Cycle = baseStoryContext.Cycle;
            saveFile.Step = baseStoryContext.Step;
            
            
            // encounters
            saveFile.EncounterDeckOrganizerState = _encounterDeckOrganizer.GetState(_jsonSerializerSettings);
            saveFile.EncounterRegistryIds = _encounterRegistry.GetAllRegisteredIds();
            
            // items
            saveFile.ItemDeckOrganizerState = _itemDeckOrganizer.GetState(_jsonSerializerSettings);
            saveFile.ItemRegistryIds = _itemRegistry.GetAllRegisteredIds();
            

            string path = _genericPathProvider.InProfileSavesPath(_sessionHolder.Session.InternalId);
            path = Path.Combine(path, nameof(BaseStorySaveFile));
            ProcedureResult result = await DiscWriting.ConvertAndWriteJson(saveFile, path, _jsonSerializerSettings, cancellationToken);
            if (result.IsFailure())
            {
                throw result.Exception;
            }
        }

        public async UniTask Load(BaseStoryContext baseStoryContext, CancellationToken cancellationToken)
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
            
            baseStoryContext.Bosses = (await saveFile.Bosses.Select(item => _encounterLoader.LoadById(item, cancellationToken)))
                .Select(item => item.GetValue())
                .Cast<BattleEncounter>()
                .ToArray();
            
            baseStoryContext.StoryEncounters = saveFile.StoryEncounters;
            baseStoryContext.Cycle = saveFile.Cycle;
            baseStoryContext.Step = saveFile.Step;
            

            // encounters
            _encounterDeckOrganizer.RestoreState(saveFile.EncounterDeckOrganizerState, _jsonSerializerSettings, _rngManager.GetRandomProvider(RNGGroup.CardShuffler));
            await _encounterRegistry.InitializeWithIds(saveFile.EncounterRegistryIds, cancellationToken);
            
            // items
            _itemDeckOrganizer.RestoreState(saveFile.ItemDeckOrganizerState, _jsonSerializerSettings, _rngManager.GetRandomProvider(RNGGroup.CardShuffler));
            await _itemRegistry.InitializeWithIds(saveFile.ItemRegistryIds, cancellationToken);
        }
        
    }
}