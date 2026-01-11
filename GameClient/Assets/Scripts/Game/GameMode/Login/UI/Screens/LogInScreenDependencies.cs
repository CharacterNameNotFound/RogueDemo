using Game.GameMode.MainHub.Controller;
using Game.Routines.ProfileOperations;
using Game.Routines.ProfileOperations.ProfileCreation;
using Game.Routines.ProfileOperations.ProfileLoading;
using Game.Session;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.InputManager;
using GameWideSystems.LocalizationWrapper;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.Login.UI.Screens
{
    public class LogInScreenDependencies : IUIScreenDependencies
    {
        public ILocalizationManager LocalizationManager { get; private set; }
        public IProfileCreationRoutine ProfileCreationRoutine { get; private set; }
        public IProfileLoadingRoutine ProfileLoadingRoutine { get; private set; }
        public GenericPathProvider GenericPathProvider { get; private set; }
        public MainHubGameMode.Factory MainHubGameModeFactory { get; private set; }
        public GameStateManager GameStateManager { get; private set; }
        public InputControlFacade InputControlFacade { get; private set; }
        public ILoadingScreenManager LoadingScreenManager { get; private set; }

        public LogInScreenDependencies(
            ILocalizationManager localizationManager, 
            IProfileCreationRoutine profileCreationRoutine, 
            IProfileLoadingRoutine profileLoadingRoutine, 
            GenericPathProvider genericPathProvider, 
            MainHubGameMode.Factory mainHubGameModeFactory, 
            GameStateManager gameStateManager, 
            InputControlFacade inputControlFacade, 
            ILoadingScreenManager loadingScreenManager)
        {
            LocalizationManager = localizationManager;
            ProfileCreationRoutine = profileCreationRoutine;
            ProfileLoadingRoutine = profileLoadingRoutine;
            GenericPathProvider = genericPathProvider;
            MainHubGameModeFactory = mainHubGameModeFactory;
            GameStateManager = gameStateManager;
            InputControlFacade = inputControlFacade;
            LoadingScreenManager = loadingScreenManager;
        }
    }
}