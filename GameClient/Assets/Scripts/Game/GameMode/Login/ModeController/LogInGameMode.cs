using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Login.UI.ScreenBuilders;
using Game.Utilities.SceneDataProviding;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.GameMode.Login.ModeController
{
    public class LogInGameMode : IGameStateController
    {
        private LogInScreenBuilder _logInScreenBuilder;
        private UIManager _uiManager;
        private ISceneAddressableDataProvider _sceneAddressableDataProvider;
        private IGameSceneManager _gameSceneManager;
        private ILoadingScreenManager _loadingScreenManager;

        public class Factory : PlaceholderFactory<LogInGameMode> { }
        
        public LogInGameMode(UIManager uiManager, LogInScreenBuilder logInScreenBuilder, ISceneAddressableDataProvider sceneAddressableDataProvider, IGameSceneManager gameSceneManager, ILoadingScreenManager loadingScreenManager)
        {
            _uiManager = uiManager;
            _logInScreenBuilder = logInScreenBuilder;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
        }

        public UniTask Initialize(GameStateInitializationParameters parameters,
            CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public async UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            await _gameSceneManager.OpenScene(_sceneAddressableDataProvider.MainScene, LoadSceneMode.Single,
                new LoadingScreenParams(false, _loadingScreenManager), cancellationToken: cancellationToken);

            await _uiManager.OpenScreenRequest(_logInScreenBuilder, null, out _).PlayWith(_uiManager, cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            return UIRequestBuilder.CloseTopRequest().PlayWith(_uiManager, cancellationToken);
        }

        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult(true);
        }
    }
}