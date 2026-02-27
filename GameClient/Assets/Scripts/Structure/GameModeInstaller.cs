using Game.GameMode.Initializer;
using Game.GameMode.Login.ModeController;
using Game.GameMode.MainHub.Controller;
using Game.GameMode.StorySelector.Controller;
using Game.GameMode.StorySession.Controller;
using Game.GameMode.StorySession.GameBoard.View;
using Zenject;

namespace Structure.GameInstalling
{
    public class GameModeInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<InitializationGameMode>().To<InitializationGameMode>().AsSingle();
            Container.BindFactory<LogInGameMode, LogInGameMode.Factory>().FromFactory<LogInGameModeFactory>();
            Container.BindFactory<MainHubGameMode, MainHubGameMode.Factory>().FromFactory<MainHubGameModeFactory>();
            
            Container.BindFactory<StorySelectorGameMode, StorySelectorGameMode.Factory>().FromFactory<StorySelectorGameModeFactory>();

            InstallStorySession();
        }

        private void InstallStorySession()
        {
            Container.BindFactory<StorySessionGameMode, StorySessionGameMode.Factory>().FromFactory<StorySessionGameModeFactory>();
            
            Container.Bind<GameBoardHolder>().To<GameBoardHolder>().AsSingle();
            
            
        }


        
    }
}