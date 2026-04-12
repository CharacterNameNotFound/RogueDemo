using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services;
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
            
            // Items and deck
            Container.Bind<IItemRegistry>().To<ItemRegistry>().AsSingle();
            Container.Bind<ItemDeckOrganizer>().To<ItemDeckOrganizer>().AsSingle();
            Container.Bind<IItemLineOrganizer>().To<ItemLineOrganizer>().AsSingle();
            Container.Bind<IItemContainersManager>().To<ItemContainersManager>().AsSingle();
            Container.Bind<IItemLoader>().To<ItemLoader>().AsSingle();
            
            Container.Bind<IItemStatGetter>().To<ItemStatGetter>().AsSingle();
            
            // Encounters
            Container.Bind<IEncounterRegistry>().To<EncounterRegistry>().AsSingle();
            Container.Bind<EncounterDeck>().To<EncounterDeck>().AsSingle();
            Container.Bind<EncounterDeckOrganizer>().To<EncounterDeckOrganizer>().AsSingle();
            Container.Bind<IEncounterLoader>().To<EncounterLoader>().AsSingle();
            

            InstallStatCalculators();
            InstallBasicStory();
        }
        
        private void InstallRoutines()
        {
            Container.Bind<InitializeStoryServicesRoutine>().To<InitializeStoryServicesRoutine>().AsSingle();
            Container.Bind<BuildAndRegisterDecksRoutine>().To<BuildAndRegisterDecksRoutine>().AsSingle();
            Container.Bind<GameBoardInitializationRoutine>().To<GameBoardInitializationRoutine>().AsSingle();
            
        }
        
        private void InstallStatCalculators()
        {
            Container.Bind<GenericStatCalculator>().To<GenericStatCalculator>().AsSingle();
            Container.Bind<IItemStatSetToItemStatValueCalculator>().To<MaxChargeStatCalculator>().AsCached();
            
        }

        private void InstallBasicStory()
        {
            Container.Bind<BaseStoryBossSelector>().To<BaseStoryBossSelector>().AsCached();
            
        }
        
        
    }
}