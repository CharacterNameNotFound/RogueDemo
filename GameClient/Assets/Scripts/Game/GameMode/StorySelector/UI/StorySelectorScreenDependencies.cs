using Game.GameMode.StorySession.Controller;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.InputManager;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.StorySelector.UI
{
    public class StorySelectorScreenDependencies : IUIScreenDependencies
    {
        public InputControlFacade InputControlFacade;
        public GameStateManager GameStateManager;
        public ILoadingScreenManager LoadingScreenManager;
        public StorySessionGameMode.Factory SessionGameModeFactory;
        
        public StorySelectorScreenDependencies(
            InputControlFacade inputControlFacade, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager, 
            StorySessionGameMode.Factory sessionGameModeFactory)
        {
            InputControlFacade = inputControlFacade;
            GameStateManager = gameStateManager;
            LoadingScreenManager = loadingScreenManager;
            SessionGameModeFactory = sessionGameModeFactory;
        }
    }
}