using Game.GameMode.StorySelector.Controller;
using Game.GameMode.StorySession.Controller;
using Game.GameMode.StorySession.Services.SaveManagement;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.InputManager;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubScreenDependencies : IUIScreenDependencies
    {
        public InputControlFacade InputControlFacade { get; }
        public GameStateManager GameStateManager { get;}
        public ILoadingScreenManager LoadingScreenManager { get; }
        public StorySelectorGameMode.Factory StorySelectorGameModeFactory { get; }
        public StorySessionGameMode.Factory StorySessionGameModeFactory { get; }
        public IGeneralSaveManager GeneralSaveManager { get; }


        public MainHubScreenDependencies(
            InputControlFacade inputControlFacade, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager, 
            StorySelectorGameMode.Factory storySelectorGameModeFactory, 
            IGeneralSaveManager generalSaveManager, 
            StorySessionGameMode.Factory storySessionGameModeFactory)
        {
            InputControlFacade = inputControlFacade;
            GameStateManager = gameStateManager;
            LoadingScreenManager = loadingScreenManager;
            StorySelectorGameModeFactory = storySelectorGameModeFactory;
            GeneralSaveManager = generalSaveManager;
            StorySessionGameModeFactory = storySessionGameModeFactory;
        }
    }
}