using Game.ManagementSystems.LookUpTableManagement;
using Game.Routines.ItemLoadingOperations;
using Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters;
using Game.Routines.ProfileOperations.ProfileCreation;
using Game.Routines.ProfileOperations.ProfileLoading;
using Game.Session;
using Zenject;

namespace Structure.GameInstalling
{
    /// <summary>
    /// Specific classes for game management
    /// </summary>
    public class GameLevelSystemInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallSessionManagement();
            InstallRoutines();
            InstallPathProviders();
            InstallDataBases();
        }

        private void InstallSessionManagement()
        {
            Container.Bind<OfflineProfileCreationValidation>().To<OfflineProfileCreationValidation>().AsSingle();
        }
        
        private void InstallRoutines()
        {
            Container.Bind<IProfileCreationRoutine>().To<OfflineProfileCreationRoutine>().AsSingle();
            Container.Bind<IProfileLoadingRoutine>().To<OfflineProfileLoadingRoutine>().AsSingle();
            
            
            Container.Bind<IItemLookUpTableLoader>().To<LoadItemFromItemSets>().AsSingle();

            InstallDataBaseFillers();
        }

        private void InstallPathProviders()
        {
            Container.Bind<GenericPathProvider>().To<GenericPathProvider>().AsSingle();
        }

        private void InstallDataBases()
        {
            Container.Bind<ILookUpTableManager>().To<LookUpTableManager>().AsSingle();
        }

        private void InstallDataBaseFillers()
        {
            Container.Bind<IItemTagToTableEntryConverter>().To<SizeItemTableConverter>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<WeaponItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<ShieldItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<HealingItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<FireItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<PoisonItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<SlowItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<HasteItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<RegenerationItemFromItemSets>().AsCached();
            Container.Bind<IItemTagToTableEntryConverter>().To<CritItemFromItemSets>().AsCached();
        }
        
        
    }
}