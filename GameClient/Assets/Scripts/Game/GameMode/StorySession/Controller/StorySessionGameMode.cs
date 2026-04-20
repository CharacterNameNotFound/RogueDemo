using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Zenject;
using Logger = GameWideSystems.Logger.Logger;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameMode : IGameStateController
    {
        
        public class Factory : PlaceholderFactory<StorySessionGameMode> { }

        private UIManager _uiManager;
        private GameStateManager _gameStateManager;
        private ILoadingScreenManager _loadingScreenManager;
        private StorySessionGameModeInitializationParameters _gmParams;
        private IStoryBase _story;
        private Logger _logger;

        public StorySessionGameMode(
            UIManager uiManager, 
            GameStateManager gameStateManager, 
            ILoadingScreenManager loadingScreenManager, 
            Logger logger)
        {
            _uiManager = uiManager;
            _gameStateManager = gameStateManager;
            _loadingScreenManager = loadingScreenManager;
            _logger = logger;
        }

        public async UniTask<bool> Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken = default)
        {
            _gmParams = (StorySessionGameModeInitializationParameters) parameters;
            _story = await _gmParams.StoryStartData.StoryID.Load<IStoryBase>(cancellationToken);
            
            ProjectContext.Instance.Container.Inject(_story);
            try
            {
                await _story.Initialize(new BaseStoryInitializationData(_gmParams.StoryStartData.CharacterID, _gmParams.TryReadSaveFile), cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Error("Failed to load game");
                _logger.Error(e.StackTrace);
                await _loadingScreenManager.Show(Application.exitCancellationToken);
                await _uiManager.CloseAllRequest().Play(Application.exitCancellationToken);
                _gameStateManager.CloseCurrentGameState(true, cancellationToken: Application.exitCancellationToken).Forget();

                return false;
            }

            return true;
        }

        public async UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            await _story.StartStory(cancellationToken);

            _story.Loop(cancellationToken).Forget();
        }

        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            // it is expected for this game mode to be most depth and to not have any alternate to swap for 
            return UniTask.CompletedTask;
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            // it is expected for this game mode to be most depth and to not have any alternate to swap for
            return UniTask.CompletedTask;
        }

        public async UniTask Close(CancellationToken cancellationToken = default)
        {
            await _story.CleanUp(cancellationToken);
            Addressables.Release(_story);
            _story = null;
        }

        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult(true);
        }
    }
}