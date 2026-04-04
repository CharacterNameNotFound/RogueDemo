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
            Container.Bind<IItemRegistry>().To<ItemRegistry>().AsSingle();
            Container.Bind<IDeckOrganizer>().To<DeckOrganizer>().AsSingle();
            Container.Bind<IItemLineOrganizer>().To<ItemLineOrganizer>().AsSingle();
            Container.Bind<IItemContainersManager>().To<ItemContainersManager>().AsSingle();
            Container.Bind<IItemLoader>().To<ItemLoader>().AsSingle();
        }
        
        private void InstallRoutines()
        {
            Container.Bind<InitializeStoryServicesRoutine>().To<InitializeStoryServicesRoutine>().AsSingle();
            Container.Bind<BuildAndRegisterDecksRoutine>().To<BuildAndRegisterDecksRoutine>().AsSingle();
            Container.Bind<GameBoardInitializationRoutine>().To<GameBoardInitializationRoutine>().AsSingle();
            
        }
        
    }
}