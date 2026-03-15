using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines;
using Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization;
using Zenject;

namespace Structure
{
    public class StoryInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallServices();
            InstallRoutines();
            
        }
        

        private void InstallServices()
        {
            Container.Bind<GameBoardHolder>().To<GameBoardHolder>().AsSingle();
            Container.Bind<ItemRegistry>().To<ItemRegistry>().AsSingle();
            Container.Bind<DeckOrganizer>().To<DeckOrganizer>().AsSingle();
            Container.Bind<ItemLineOrganizer>().To<ItemLineOrganizer>().AsSingle();
            Container.Bind<ItemContainersManager>().To<ItemContainersManager>().AsSingle();
        }
        
        private void InstallRoutines()
        {
            Container.Bind<InitializeStoryServicesRoutine>().To<InitializeStoryServicesRoutine>().AsSingle();
            Container.Bind<BuildAndRegisterDecksRoutine>().To<BuildAndRegisterDecksRoutine>().AsSingle();
            Container.Bind<GameBoardInitializationRoutine>().To<GameBoardInitializationRoutine>().AsSingle();
            
        }
        
    }
}