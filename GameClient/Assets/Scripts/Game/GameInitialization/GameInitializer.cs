using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Cheats;
using Game.GameMode.Initializer;
using GameWideSystems.AudioManager;
using GameWideSystems.GameStateManagement;
using GameWideSystems.LocalizationWrapper;
using UnityEngine;
using Zenject;

namespace Game.GameInitialization
{
    public class GameInitializer : MonoBehaviour
    {
        private AudioManager _audioManager;
        private InitializationGameMode _initializationGameMode;
        private GameStateManager _gameStateManager;
        private ILocalizationManager _localizationManager;
        private CheatConsoleController _cheatConsoleController;
        
        [Inject]
        private void Construct(
            AudioManager audioManager, 
            InitializationGameMode initializationGameMode, 
            GameStateManager gameStateManager, 
            ILocalizationManager localizationManager, 
            CheatConsoleController cheatConsoleController)
        {
            _audioManager = audioManager;
            _initializationGameMode = initializationGameMode;
            _gameStateManager = gameStateManager;
            _localizationManager = localizationManager;
            _cheatConsoleController = cheatConsoleController;
        }
        
        private void Start()
        {
            Initialize(Application.exitCancellationToken).Forget();
        }
        
        private async UniTask Initialize(CancellationToken cancellationToken)
        {
            Transform proceduralHolderTransform = FindAnyObjectByType<ProjectContext>().transform;

            await _cheatConsoleController.Initialize();
            await _audioManager.Initialize(proceduralHolderTransform, cancellationToken);
            await _localizationManager.Initialize();

            await _gameStateManager.AppendGameState(_initializationGameMode, cancellationToken: cancellationToken);
        }
        
    }
}