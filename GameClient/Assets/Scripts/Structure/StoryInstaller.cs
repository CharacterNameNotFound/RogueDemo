using Game.GameMode.StorySession.GameBoard.Services.BoardModelManipulation;
using Game.GameMode.StorySession.GameBoard.Services.EventHandling;
using Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.Services.ItemDescriptionBuilding;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters;
using Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating;
using Game.GameMode.StorySession.GameBoard.Services.TextsDrawing;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Facades;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.Services.SaveManagement;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles.Routines;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.ItemRaritySelection;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.Utilities;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.PlayerStashEncounter;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Routines.Merchant;
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
            InstallSimulationEventHandling();
        }

        private void InstallServices()
        {
            Container.Bind<IGeneralSaveManager>().To<GeneralSaveManager>().AsSingle();
            Container.Bind<IStoryFinalizer>().To<StoryFinalizer>().AsSingle();
            Container.Bind<IEncounterSelector>().To<EncounterSelector>().AsSingle();
            Container.Bind<IEncounterPlayer>().To<EncounterPlayer>().AsSingle();
            Container.Bind<IItemManipulator>().To<ItemManipulator>().AsSingle();
            Container.Bind<IItemPresenter>().To<ItemPresenter>().AsSingle();
            Container.Bind<IItemUpgrader>().To<ItemUpgrader>().AsSingle();
            Container.Bind<IItemRemover>().To<ItemRemover>().AsSingle();
            Container.Bind<IItemTransactionOperationController>().To<ItemTransactionOperationController>().AsSingle();
            
            // views and model
            Container.Bind<GameBoardHolder>().To<GameBoardHolder>().AsSingle();
            Container.Bind<IStoryContextProvider>().To<StoryContextHolder>().AsSingle();
            Container.Bind<IGameBoardModelHolder>().To<GameBoardModelHolder>().AsSingle();
            Container.Bind<IItemDescriptionBuilder>().To<ItemDescriptionBuilder>().AsSingle();
            Container.Bind<ISessionStatusDrawer>().To<SessionStatusDrawer>().AsSingle();
            Container.Bind<IPlayerStatusUpdater>().To<PlayerStatusUpdater>().AsSingle();
            Container.Bind<IBoardModelManipulator>().To<BoardModelManipulator>().AsSingle();
            Container.Bind<IGameBoardModelCreator>().To<GameBoardModelCreator>().AsSingle();
            Container.Bind<IHeroesHpDrawer>().To<HeroesHpDrawer>().AsSingle();
            
            // Items and deck
            Container.Bind<IItemRegistry>().To<ItemRegistry>().AsSingle();
            Container.Bind<ItemDeckOrganizer>().To<ItemDeckOrganizer>().AsSingle();
            Container.Bind<IItemLineOrganizer>().To<ItemLineOrganizer>().AsSingle();
            Container.Bind<IItemContainersManager>().To<ItemContainersManager>().AsSingle();
            Container.Bind<IItemLoader>().To<ItemLoader>().AsSingle();
            Container.Bind<IItemIdCollector>().To<ItemIdCollector>().AsSingle();
            
            Container.Bind<IItemStatGetter>().To<ItemStatGetter>().AsSingle();
            
            Container.Bind<IPlayerStashController>().To<PlayerStashController>().AsSingle();
            
            // Encounters
            Container.Bind<IEncounterRegistry>().To<EncounterRegistry>().AsSingle();
            Container.Bind<EncounterDeck>().To<EncounterDeck>().AsSingle();
            Container.Bind<EncounterDeckOrganizer>().To<EncounterDeckOrganizer>().AsSingle();
            Container.Bind<IEncounterLoader>().To<EncounterLoader>().AsSingle();
            Container.Bind<IMerchantEncounterRoutines>().To<MerchantEncounterRoutines>().AsSingle();
            Container.Bind<IBattleEncounterRoutines>().To<BattleEncounterRoutines>().AsSingle();
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
            Container.Bind<BaseStoryCycleGenerator>().To<BaseStoryCycleGenerator>().AsSingle();
            
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
            
            Container.Bind<IEventDispatcher<PreFightArguments>>().To<EventDispatcher<PreFightArguments>>().AsSingle();
            Container.Bind<IEventDispatcher<PostFightArguments>>().To<EventDispatcher<PostFightArguments>>().AsSingle();
        }
        
        private void InstallBalancing()
        {
            Container.Bind<IItemRaritySelector>().To<ItemRaritySelector>().AsSingle();
        }
        
        private void InstallSimulation()
        {
            Container.Bind<IItemRenderingFacade>().To<ItemRenderingFacade>().AsSingle();
            Container.Bind<ISimulationPlayer>().To<SimulationPlayer>().AsSingle();
            Container.Bind<ISimulationLoop>().To<SimulationLoop>().AsSingle();
            Container.Bind<ISimulationViewUpdater>().To<SimulationViewUpdater>().AsSingle();
            Container.Bind<ISimulationModelUpdater>().To<SimulationModelUpdater>().AsSingle();
            Container.Bind<IWinDecisionMaker>().To<WinDecisionMaker>().AsSingle();
        }
        
        private void InstallSimulationEventHandling()
        {
            Container.Bind<IMoveEventHandler>().To<MoveEventHandler>().AsSingle().NonLazy();
            Container.Bind<IPurchaseEventHandler>().To<PurchaseEventHandler>().AsSingle().NonLazy();
            Container.Bind<ISellEventHandler>().To<SellEventHandler>().AsSingle().NonLazy();
            Container.Bind<IUpgradeEventHandler>().To<UpgradeEventHandler>().AsSingle().NonLazy();
        }
        
        
    }
}