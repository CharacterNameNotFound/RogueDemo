using Game.GameMode.StorySelector.Controller;
using Game.GameSaveSystem;
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
        public IBlobManager BlobManager { get; }
        public StorySelectorGameMode.Factory StorySelectorGameModeFactory { get; }


        public MainHubScreenDependencies(
            InputControlFacade inputControlFacade, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager, 
            IBlobManager blobManager, 
            StorySelectorGameMode.Factory storySelectorGameModeFactory)
        {
            InputControlFacade = inputControlFacade;
            GameStateManager = gameStateManager;
            LoadingScreenManager = loadingScreenManager;
            BlobManager = blobManager;
            StorySelectorGameModeFactory = storySelectorGameModeFactory;
        }
    }
}