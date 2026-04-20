using Game.GameMode.StorySession.Controller;
using Game.GameMode.StorySession.Services.SaveManagement;
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
        public IGeneralSaveManager GeneralSaveManager;
        
        
        public StorySelectorScreenDependencies(
            InputControlFacade inputControlFacade, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager, 
            StorySessionGameMode.Factory sessionGameModeFactory, 
            IGeneralSaveManager generalSaveManager)
        {
            InputControlFacade = inputControlFacade;
            GameStateManager = gameStateManager;
            LoadingScreenManager = loadingScreenManager;
            SessionGameModeFactory = sessionGameModeFactory;
            GeneralSaveManager = generalSaveManager;
        }
    }
}