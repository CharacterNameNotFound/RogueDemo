using Game.GameMode.Initializer;
using Game.GameMode.Login.ModeController;
using Game.GameMode.MainHub.Controller;
using Game.GameMode.StorySelector.Controller;
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
            
        }


        
    }
}