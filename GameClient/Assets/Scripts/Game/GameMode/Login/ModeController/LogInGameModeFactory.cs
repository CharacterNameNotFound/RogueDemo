using Game.GameMode.Login.UI.ScreenBuilders;
using Game.Utilities.SceneDataProviding;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.UIManagement;
using Zenject;

namespace Game.GameMode.Login.ModeController
{
    public class LogInGameModeFactory : IFactory<LogInGameMode>
    {
        private LogInScreenBuilder _logInScreenBuilder;
        private UIManager _uiManager;
        private ISceneAddressableDataProvider _sceneAddressableDataProvider;
        private IGameSceneManager _gameSceneManager;
        private ILoadingScreenManager _loadingScreenManager;

        public LogInGameModeFactory(LogInScreenBuilder logInScreenBuilder, UIManager uiManager, ISceneAddressableDataProvider sceneAddressableDataProvider, IGameSceneManager gameSceneManager, ILoadingScreenManager loadingScreenManager)
        {
            _logInScreenBuilder = logInScreenBuilder;
            _uiManager = uiManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _gameSceneManager = gameSceneManager;
            _loadingScreenManager = loadingScreenManager;
        }

        public LogInGameMode Create()
        {
            return new LogInGameMode(_uiManager, _logInScreenBuilder, _sceneAddressableDataProvider, _gameSceneManager, _loadingScreenManager);
        }
    }
}