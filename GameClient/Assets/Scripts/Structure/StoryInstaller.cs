using Game.GameMode.StorySession.GameBoard.Services.GameBoardManagement;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Services.ItemDescriptionBuilding;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters;
using Game.GameMode.StorySession.GameBoard.Simulation.Facades;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.Services.SaveManagement;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Battle;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.ItemRaritySelection;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.Utilities;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.PlayerStashEncounter;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection;
using Game.GameMode.StorySession.StoryLoop.Services.InputControl;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.StoryFinalization;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services.GameSaving;
using Game.GameMode.StorySession.Utilities.EventArguments;
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
            InstallSimulation();
        }

        private void InstallServices()
        {
            Container.Bind<IGeneralSaveManager>().To<GeneralSaveManager>().AsSingle();
            Container.Bind<IStoryFinalizer>().To<StoryFinalizer>().AsSingle();
            Container.Bind<IEncounterSelector>().To<EncounterSelector>().AsSingle();
            Container.Bind<IEncounterPlayer>().To<EncounterPlayer>().AsSingle();
            Container.Bind<IItemManipulator>().To<ItemManipulator>().AsSingle();
            Container.Bind<IItemPresenter>().To<ItemPresenter>().AsSingle();
            Container.Bind<IItemTransactionOperationController>().To<ItemTransactionOperationController>().AsSingle();
            
            // views and model
            Container.Bind<GameBoardHolder>().To<GameBoardHolder>().AsSingle();
            Container.Bind<IStoryContextProvider>().To<StoryContextHolder>().AsSingle();
            Container.Bind<IItemDescriptionBuilder>().To<ItemDescriptionBuilder>().AsSingle();
            
            // Items and deck
            Container.Bind<IItemRegistry>().To<ItemRegistry>().AsSingle();
            Container.Bind<ItemDeckOrganizer>().To<ItemDeckOrganizer>().AsSingle();
            Container.Bind<IItemLineOrganizer>().To<ItemLineOrganizer>().AsSingle();
            Container.Bind<IItemContainersManager>().To<ItemContainersManager>().AsSingle();
            Container.Bind<IItemLoader>().To<ItemLoader>().AsSingle();
            Container.Bind<IItemBoardModelUpdater>().To<ItemBoardModelUpdater>().AsSingle();
            Container.Bind<IItemIdCollector>().To<ItemIdCollector>().AsSingle();
            
            Container.Bind<IItemStatGetter>().To<ItemStatGetter>().AsSingle();
            
            Container.Bind<IPlayerStashController>().To<PlayerStashController>().AsSingle();
            
            // Encounters
            Container.Bind<IEncounterRegistry>().To<EncounterRegistry>().AsSingle();
            Container.Bind<EncounterDeck>().To<EncounterDeck>().AsSingle();
            Container.Bind<EncounterDeckOrganizer>().To<EncounterDeckOrganizer>().AsSingle();
            Container.Bind<IEncounterLoader>().To<EncounterLoader>().AsSingle();
            Container.Bind<IMerchantEncounterRoutines>().To<MerchantEncounterRoutines>().AsSingle();
            Container.Bind<IBattleEncounterPlayer>().To<BattleEncounterPlayer>().AsSingle();
            Container.Bind<IMerchantItemExclusionListBuilder>().To<MerchantItemExclusionListBuilder>().AsSingle();
            

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
            Container.Bind<IInputLayerControlMediator>().To<InputLayerControlMediator>().AsSingle();
            
            // Layers
            Container.Bind<ItemManipulationInputLayer>().To<ItemManipulationInputLayer>().AsSingle();
            Container.Bind<ItemDetailsInputLayers>().To<ItemDetailsInputLayers>().AsSingle();
            
            Container.Bind<StorySessionEncounterSelectionInputLayer>()
                .To<StorySessionEncounterSelectionInputLayer>()
                .AsSingle();
        }
        
        private void InstallEvents()
        {
            Container.Bind<IItemTransactionEventPublisher>().To<ItemTransactionEventPublisher>().AsSingle();
            
            Container.Bind<IEventDispatcher<PostItemMovementArguments>>().To<EventDispatcher<PostItemMovementArguments>>().AsSingle();
            Container.Bind<IEventDispatcher<PreItemMovementArguments>>().To<EventDispatcher<PreItemMovementArguments>>().AsSingle();
            
            Container.Bind<IEventDispatcher<PreItemSellArguments>>().To<EventDispatcher<PreItemSellArguments>>().AsSingle();
            Container.Bind<IEventDispatcher<PostItemSellArguments>>().To<EventDispatcher<PostItemSellArguments>>().AsSingle();
            
            Container.Bind<IEventDispatcher<PreItemPurchaseArguments>>().To<EventDispatcher<PreItemPurchaseArguments>>().AsSingle();
            Container.Bind<IEventDispatcher<PostItemPurchaseArguments>>().To<EventDispatcher<PostItemPurchaseArguments>>().AsSingle();
            
            Container.Bind<IEventDispatcher<PreItemUpgradeArgument>>().To<EventDispatcher<PreItemUpgradeArgument>>().AsSingle();
            Container.Bind<IEventDispatcher<PostItemUpgradeArguments>>().To<EventDispatcher<PostItemUpgradeArguments>>().AsSingle();
        }
        
        private void InstallBalancing()
        {
            Container.Bind<IItemRaritySelector>().To<ItemRaritySelector>().AsSingle();
        }
        
        private void InstallSimulation()
        {
            Container.Bind<IItemRenderingFacade>().To<ItemRenderingFacade>().AsSingle();
        }
        
        
    }
}