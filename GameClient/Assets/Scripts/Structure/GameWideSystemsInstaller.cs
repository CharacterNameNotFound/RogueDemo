using Game.GameSaveSystem;
using GameWideSystems.AudioManager;
using GameWideSystems.CameraManagement;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.GameSceneManager.LoadingScreen;
using GameWideSystems.GameStateManagement;
using GameWideSystems.LocalizationWrapper;
using GameWideSystems.RNGManagement;
using GameWideSystems.SessionManagement.Sessions;
using GameWideSystems.UIManagement;
using UnityEngine;
using Utils.UtilityTypes.ObjectPooling;
using Zenject;
using Logger = GameWideSystems.Logger.Logger;

namespace Structure.GameInstalling
{
    // Somewhat universal classes
    public class GameWideSystemsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AudioManagerConfigurations _audioManagerConfigurations;
        [SerializeField] private GameObject _uiManagementPrefab;
        [SerializeField] private GameObject _cameraManager;
        [SerializeField] private GenericPooledObjectHostProvider _genericPooledObjectHostProvider;
        
        public override void InstallBindings()
        {
            InstallLogger();
            InstallAudioManager();
            InstallGameSceneManager();
            InstallGameStateManager();
            InstallLocalization();
            InstallUIManager();
            InstallSession();
            InstallGenericHosts();
            InstallCameraManager();
            InstallRandom();
        }

        private void InstallGenericHosts()
        {
            Container.Bind<IPooledObjectHostProvider>().FromComponentInNewPrefab(_genericPooledObjectHostProvider).AsSingle().NonLazy();
        }

        private void InstallSession()
        {
            Container.Bind<SessionHolder>().To<SessionHolder>().AsSingle();
            Container.Bind<IBlobManager>().To<BlobManager>().AsSingle();
            Container.Bind<BlobReader>().To<BlobReader>().AsSingle();
            Container.Bind<BlobWriter>().To<BlobWriter>().AsSingle();
        }

        private void InstallUIManager()
        {
            Container.Bind<IScreenHostProvider>().FromComponentInNewPrefab(_uiManagementPrefab).AsSingle().NonLazy();
            Container.Bind<UIManager>().To<UIManager>().AsSingle();
        }

        private void InstallLocalization()
        {
            Container.Bind<ILocalizationManager>().To<LocalizationManager>().AsSingle();
        }

        private void InstallLogger()
        {
            Container.Bind<Logger>().ToSelf().AsSingle();
        }

        private void InstallAudioManager()
        {
            Container.Bind<AudioManagerConfigurations>().FromInstance(_audioManagerConfigurations).AsSingle();
            Container.Bind<AudioManager>().To<AudioManager>().AsSingle().NonLazy();
        }
        
        private void InstallGameSceneManager()
        {
            Container.Bind<IGameSceneManager>().To<GameSceneManager>().AsSingle().NonLazy();
            Container.Bind<ILoadingScreenManager>().To<BasicLoadingScreenManager>().AsSingle();
        }

        private void InstallGameStateManager()
        {
            Container.Bind<GameStateManager>().To<GameStateManager>().AsSingle();
        }

        private void InstallCameraManager()
        {
            Container.Bind<ICameraManager>().FromComponentInNewPrefab(_cameraManager).AsSingle();
        }
        
        private void InstallRandom()
        {
            Container.Bind<IRNGManager>().To<RNGManager>().AsSingle();
        }
        
    }
}