using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using Zenject;
using Logger = GameWideSystems.Logger.Logger;


namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameModeFactory : IFactory<StorySessionGameMode>
    {
        private UIManager _uiManager;
        private GameStateManager _gameStateManager;
        private ILoadingScreenManager _loadingScreenManager;
        private Logger _logger;
        
        public StorySessionGameModeFactory(
            UIManager uiManager, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager, 
            Logger logger)
        {
            _uiManager = uiManager;
            _gameStateManager = gameStateManager;
            _loadingScreenManager = loadingScreenManager;
            _logger = logger;
        }

        public StorySessionGameMode Create()
        {
            return new StorySessionGameMode(_uiManager, _gameStateManager, _loadingScreenManager, _logger);
        }
    }
}