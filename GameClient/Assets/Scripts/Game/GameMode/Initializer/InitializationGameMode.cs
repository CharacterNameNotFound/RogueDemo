using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Login.ModeController;
using Game.ManagementSystems.LookUpTableManagement;
using Game.Routines.ItemLoadingOperations;
using Game.Session;
using Game.UI.Tooltips;
using GameWideSystems.CameraManagement;
using GameWideSystems.GameStateManagement;
using UnityEngine;

namespace Game.GameMode.Initializer
{
    public class InitializationGameMode : IGameStateController
    {
        private GenericPathProvider _genericPathProvider;
        private GameStateManager _gameStateManager;
        private LogInGameMode.Factory _logInGameModeFactory;
        private TextTooltipRegisterer _textTooltipRegisterer;
        private ICameraManager _cameraManager;
        private ILookUpTableManager _lookUpTableManager;
        private IItemLookUpTableLoader _itemLookUpTableLoader;

        public InitializationGameMode(
            GenericPathProvider genericPathProvider, 
            GameStateManager gameStateManager, 
            LogInGameMode.Factory logInGameModeFactory, 
            TextTooltipRegisterer textTooltipRegisterer, 
            ICameraManager cameraManager, 
            ILookUpTableManager lookUpTableManager, 
            IItemLookUpTableLoader itemLookUpTableLoader)
        {
            _genericPathProvider = genericPathProvider;
            _gameStateManager = gameStateManager;
            _logInGameModeFactory = logInGameModeFactory;
            _textTooltipRegisterer = textTooltipRegisterer;
            _cameraManager = cameraManager;
            _lookUpTableManager = lookUpTableManager;
            _itemLookUpTableLoader = itemLookUpTableLoader;
        }

        public async UniTask Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken)
        {
            _cameraManager.Initialize();
            await TryInitializeFirstRun(Application.exitCancellationToken);
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
        
        // first run references first run for session, as this game mode is a fallback for soft restart 
        private async UniTask TryInitializeFirstRun(CancellationToken cancellationToken)
        {
            if (!Directory.Exists(_genericPathProvider.SaveFilesPath()))
            {
                Directory.CreateDirectory(_genericPathProvider.SaveFilesPath());
            }

            await _lookUpTableManager.LoadLookUpTables(cancellationToken);
            await _itemLookUpTableLoader.BuildItemLookUp(cancellationToken);
        }
        
    }
}