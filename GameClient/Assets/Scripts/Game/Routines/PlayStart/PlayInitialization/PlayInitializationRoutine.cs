using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameSaveSystem;
using Game.Utilities.SceneDataProviding;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameSceneManager;
using GameWideSystems.InputManager;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Utils.UtilityTypes.Result;

namespace Game.Routines.PlayStart.PlayInitialization
{
    public class PlayInitializationRoutine : IPlayInitializationRoutine
    {
        private IGameSceneManager _gameSceneManager;
        private ISceneAddressableDataProvider _sceneAddressableDataProvider;
        private ILoadingScreenManager _loadingScreenManager;
        private InputControlFacade _inputControlFacade;
        private IBlobManager _blobManager;
        private IStartingPlayerConfigs _startingPlayerConfigs;

        public PlayInitializationRoutine(
            IGameSceneManager gameSceneManager, 
            ISceneAddressableDataProvider sceneAddressableDataProvider, 
            ILoadingScreenManager loadingScreenManager, 
            InputControlFacade inputControlFacade, 
            IBlobManager blobManager, 
            IStartingPlayerConfigs startingPlayerConfigs)
        {
            _gameSceneManager = gameSceneManager;
            _sceneAddressableDataProvider = sceneAddressableDataProvider;
            _loadingScreenManager = loadingScreenManager;
            _inputControlFacade = inputControlFacade;
            _blobManager = blobManager;
            _startingPlayerConfigs = startingPlayerConfigs;
        }

        public async UniTask<ProcedureResult> InitializeSession(CancellationToken cancellationToken)
        {
            _inputControlFacade.SetInputsAvailable(false);
            
            await _gameSceneManager.OpenScene(_sceneAddressableDataProvider.SpaceAdventureScene, LoadSceneMode.Single,
                new LoadingScreenParams(false, _loadingScreenManager), cancellationToken: cancellationToken);


            await _blobManager.WriteBlob(cancellationToken);

            await _loadingScreenManager.Hide(true, cancellationToken);
            _inputControlFacade.SetInputsAvailable(true);
            
            return ProcedureResultBuilder.Success();
        }
        
        
    }
}