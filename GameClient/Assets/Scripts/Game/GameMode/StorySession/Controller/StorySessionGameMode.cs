using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.UI;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using Utils.UtilityTypes.AssetReferencing;
using Zenject;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameMode : IGameStateController
    {
        
        public class Factory : PlaceholderFactory<StorySessionGameMode> { }
        
        private UIManager _uiManager;
        private ILoadingScreenManager _loadingScreenManager;
        private StorySessionScreenBuilder _storySessionScreenBuilder;

        private StorySessionGameModeInitializationParameters _gmParams;
        private IStoryBase _story;

        public StorySessionGameMode(
            UIManager uiManager, 
            ILoadingScreenManager loadingScreenManager, 
            StorySessionScreenBuilder storySessionScreenBuilder)
        {
            _uiManager = uiManager;
            _loadingScreenManager = loadingScreenManager;
            _storySessionScreenBuilder = storySessionScreenBuilder;
        }

        public async UniTask Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken = default)
        {
            _gmParams = (StorySessionGameModeInitializationParameters) parameters;
            _story = await _gmParams.StoryStartData.StoryID.ToAssetReferenceDto().Load<IStoryBase>(cancellationToken);
            
            ProjectContext.Instance.Container.Inject(_story);
            await _story.StartStory(cancellationToken);
        }

        public async UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            // PlayWith не нужно передавать ЮИ менеджер
            await _uiManager.OpenScreenRequest(_storySessionScreenBuilder, null, out _).PlayWith(_uiManager, cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);
        }

        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult(true);
        }
    }
}