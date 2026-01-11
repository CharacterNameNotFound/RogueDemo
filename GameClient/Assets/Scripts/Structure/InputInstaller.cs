using GameWideSystems.InputManager;
using GameWideSystems.InputManager.ReadingCores;
using GameWideSystems.InputManager.ReadingCores.Keyboard;
using GameWideSystems.InputManager.ReadingCores.Pointer;
using UnityEngine;
using Zenject;

namespace Structure.GameInstalling
{
    public class InputInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PointerInputConfigurationsSOProviders _pointerInputConfigurationsSoProviders;
        
        public override void InstallBindings()
        {
            InstallInputs();
        }
        
        private void InstallInputs()
        {
            Container.Bind<IPointerInputConfigurationsProvider>().FromInstance(_pointerInputConfigurationsSoProviders).AsSingle();

            Container.Bind<MainInputs>().To<MainInputs>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInputHost>().To<InputHost>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInputReadingCore>().To<PointerCore>().FromNew().AsCached().NonLazy();
            Container.Bind<IInputReadingCore>().To<KeyboardReader>().FromNew().AsCached().NonLazy();
            Container.Bind<InputReader>().To<InputReader>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<InputControlFacade>().To<InputControlFacade>().FromNew().AsSingle();

            Container.BindInterfacesTo<IInputReadingCore>().FromResolveAll().AsCached();
        }
        
    }
}