using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines;
using Game.GameMode.StorySession.UI;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using UnityEngine;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class BaseStory : ScriptableObject, IStoryBase
    {
        private BuildAndRegisterDecksRoutine _buildAndRegisterItemsRoutine;
        private ILoadingScreenManager _loadingScreenManager;
        private BaseStoryConfigs _baseStoryConfigs;
        private UIManager _uiManager;
        private StorySessionScreenBuilder _storySessionScreenBuilder;
        private GameBoardInitializationRoutine _boardInitializationRoutine;
        private InitializeStoryServicesRoutine _initializeStoryServicesRoutine;


        private StoryInitializationData _storyInitializationData;

        [Inject]
        private void InjectDependencies(
            ILoadingScreenManager loadingScreenManager,
            BaseStoryConfigs baseStoryConfigs,
            BuildAndRegisterDecksRoutine buildAndRegisterDecksRoutine,
            UIManager uiManager,
            StorySessionScreenBuilder storySessionScreenBuilder,
            GameBoardInitializationRoutine boardInitializationRoutine,
            InitializeStoryServicesRoutine initializeStoryServicesRoutine
            )
        {
            _loadingScreenManager = loadingScreenManager;
            _baseStoryConfigs = baseStoryConfigs;
            _buildAndRegisterItemsRoutine = buildAndRegisterDecksRoutine;
            _uiManager = uiManager;
            _storySessionScreenBuilder = storySessionScreenBuilder;
            _boardInitializationRoutine = boardInitializationRoutine;
            _initializeStoryServicesRoutine = initializeStoryServicesRoutine;
        }

        public async UniTask Initialize(StoryInitializationData storyInitializationData, CancellationToken cancellationToken)
        {
            _storyInitializationData = storyInitializationData;
            
            
            // Loading game board
            await _boardInitializationRoutine.Initialize(cancellationToken);
            
            // generate object pools
            await _initializeStoryServicesRoutine.InitializePools(cancellationToken);
            
            
            // generate and save curses
            // Empty for now
            
            
            // generate encounters
            
            
            
            // generate decks
            await _buildAndRegisterItemsRoutine.BuildBasicItemsAndRegistries(
                _baseStoryConfigs.NeutralItemSets, 
                storyInitializationData.CharacterData.ItemSets, 
                _baseStoryConfigs, 
                _baseStoryConfigs, 
                cancellationToken);
            

            // generate bosses

            
            // save decks, events, bosses

            
            // build first cycle and save


        }

        public async UniTask StartStory(CancellationToken cancellationToken)
        {
            await _uiManager.OpenScreenRequest(_storySessionScreenBuilder, null, out _).Play(cancellationToken);

            
            
            // removing loading screen
            await _loadingScreenManager.Hide(true, cancellationToken);
            //////////////////////////
            
            // playing bosses animation
            
            
            // play entrance animation
            
            
            
        }

        public UniTask Load(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public UniTask Loop(CancellationToken cancellationToken)
        {
            // save each cycle
            throw new System.NotImplementedException();
        }

        public UniTask Finish(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public UniTask CleanUp(CancellationToken cancellationToken)
        {
            
            
            return UniTask.CompletedTask;
        }
        
    }
}