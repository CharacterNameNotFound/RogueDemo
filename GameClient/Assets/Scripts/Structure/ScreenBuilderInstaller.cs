using Game.GameMode.Login.UI.ScreenBuilders;
using Game.GameMode.Login.UI.Screens;
using Game.GameMode.MainHub.UI.Screen;
using Game.GameMode.StorySelector.UI;
using Game.GameMode.StorySession.UI;
using Zenject;

namespace Structure.GameInstalling
{
    public class ScreenBuilderInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallLogInScreen();
            InstallMainScreen();

            InstallStorySelection();
            InstallStorySession();
        }

        private void InstallLogInScreen()
        {
            Container.Bind<LogInScreenBuilder>().To<LogInScreenBuilder>().AsSingle();
            Container.Bind<LogInScreenDependencies>().To<LogInScreenDependencies>().AsSingle();
        }

        private void InstallMainScreen()
        {
            Container.Bind<MainHubScreenBuilder>().To<MainHubScreenBuilder>().AsSingle();
            Container.Bind<MainHubScreenDependencies>().To<MainHubScreenDependencies>().AsSingle();
        }
        
        private void InstallStorySelection()
        {
            Container.Bind<StorySelectorScreenBuilder>().To<StorySelectorScreenBuilder>().AsSingle();
            Container.Bind<StorySelectorScreenDependencies>().To<StorySelectorScreenDependencies>().AsSingle();
        }
        
        private void InstallStorySession()
        {
            Container.Bind<StorySessionScreenBuilder>().To<StorySessionScreenBuilder>().AsSingle();
            Container.Bind<StorySessionScreenDependencies>().To<StorySessionScreenDependencies>().AsSingle();
        }

        
    }
}