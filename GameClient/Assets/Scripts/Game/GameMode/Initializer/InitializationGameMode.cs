using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Login.ModeController;
using Game.Session;
using Game.UI.Tooltips;
using GameWideSystems.CameraManagement;
using GameWideSystems.GameStateManagement;

namespace Game.GameMode.Initializer
{
    public class InitializationGameMode : IGameStateController
    {
        private GenericPathProvider _genericPathProvider;
        private GameStateManager _gameStateManager;
        private LogInGameMode.Factory _logInGameModeFactory;
        private TextTooltipRegisterer _textTooltipRegisterer;
        private ICameraManager _cameraManager;

        public InitializationGameMode(GenericPathProvider genericPathProvider, GameStateManager gameStateManager, LogInGameMode.Factory logInGameModeFactory, TextTooltipRegisterer textTooltipRegisterer, ICameraManager cameraManager)
        {
            _genericPathProvider = genericPathProvider;
            _gameStateManager = gameStateManager;
            _logInGameModeFactory = logInGameModeFactory;
            _textTooltipRegisterer = textTooltipRegisterer;
            _cameraManager = cameraManager;
        }

        public UniTask Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken)
        {
            _cameraManager.Initialize();
            TryInitializeFirstRun();
            return UniTask.CompletedTask;
        }

        public UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            _textTooltipRegisterer.Register();
            return _gameStateManager.AppendGameState(_logInGameModeFactory.Create(), cancellationToken: cancellationToken);
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }
        
        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            return _gameStateManager.AppendGameState(_logInGameModeFactory.Create(), cancellationToken: cancellationToken);
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            throw new Exception("Attempting to close game initializer");
        }
                      
        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult<bool>(false);
        }
        
        private void TryInitializeFirstRun()
        {
            if (!Directory.Exists(_genericPathProvider.SaveFilesPath()))
            {
                Directory.CreateDirectory(_genericPathProvider.SaveFilesPath());
            }
        }
        
    }
}