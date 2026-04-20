using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.Services.SaveManagement;
using Game.GameMode.StorySession.StoryLoop.Encounters.Merchants.ItemRaritySelection;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Battle;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.StoryFinalization;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services.GameSaving;
using Utils.UtilityTypes.EventProcessing;
using Zenject;

namespace Structure
{
    public class StoryInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallServices();
            InstallRoutines();
            InstallInputs();
            InstallEvents();
            InstallBalancing();
        }
        

        private void InstallServices()
        {
            Container.Bind<IGeneralSaveManager>().To<GeneralSaveManager>().AsSingle();
            Container.Bind<IStoryFinalizer>().To<StoryFinalizer>().AsSingle();
            Container.Bind<IEncounterSelector>().To<EncounterSelector>().AsSingle();
            Container.Bind<IEncounterPlayer>().To<EncounterPlayer>().AsSingle();
            Container.Bind<IItemManipulator>().To<ItemManipulator>().AsSingle();
            Container.Bind<IItemPresenter>().To<ItemPresenter>().AsSingle();
            
            // views
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
            Container.Bind<IMerchantEncounterPlayer>().To<MerchantEncounterPlayer>().AsSingle();
            Container.Bind<IBattleEncounterPlayer>().To<BattleEncounterPlayer>().AsSingle();
            

            InstallStatCalculators();
            InstallBasicStory();
        }
        
        private void InstallRoutines()
        {
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
            Container.Bind<BaseStoryBossSelector>().To<BaseStoryBossSelector>().AsSingle();
            Container.Bind<BaseStorySaveManager>().To<BaseStorySaveManager>().AsSingle();
            Container.Bind<BaseStoryDayGenerator>().To<BaseStoryDayGenerator>().AsSingle();
            
        }

        private void InstallInputs()
        {
            Container.Bind<ItemManipulationInputLayer>().To<ItemManipulationInputLayer>().AsSingle();
            
            Container.Bind<StorySessionEncounterSelectionInputLayer>()
                .To<StorySessionEncounterSelectionInputLayer>()
                .AsSingle();
        }
        
        private void InstallEvents()
        {
            Container.Bind<IEventDispatcher<EventArgs>>().To<EventDispatcher<EventArgs>>().AsSingle();
        }
        
        private void InstallBalancing()
        {
            Container.Bind<IItemRaritySelector>().To<ItemRaritySelector>().AsSingle();
        }
        
    }
}