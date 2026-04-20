using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySelector.UI;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using Zenject;

namespace Game.GameMode.StorySelector.Controller
{
    public class StorySelectorGameMode : IGameStateController
    {
        public class Factory : PlaceholderFactory<StorySelectorGameMode> { }

        private UIManager _uiManager;
        private ILoadingScreenManager _loadingScreenManager;
        private StorySelectorScreenBuilder _storySelectorScreenBuilder;

        public StorySelectorGameMode(
            UIManager uiManager, 
            ILoadingScreenManager loadingScreenManager, 
            StorySelectorScreenBuilder storySelectorScreenBuilder)
        {
            _uiManager = uiManager;
            _loadingScreenManager = loadingScreenManager;
            _storySelectorScreenBuilder = storySelectorScreenBuilder;
        }


        public UniTask<bool> Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken = default)
        {
            return UniTask.FromResult(true);
        }

        public async UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            await _uiManager.OpenScreenRequest(_storySelectorScreenBuilder, null, out _).Play(cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);
        }

        public async UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            await _uiManager.OpenScreenRequest(_storySelectorScreenBuilder, null, out _).Play(cancellationToken);
            await _loadingScreenManager.Hide(true, cancellationToken);
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return Close(cancellationToken);
        }

        public async UniTask Close(CancellationToken cancellationToken = default)
        {
            await _uiManager.CloseTopRequest().Play(cancellationToken);
        }

        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult(true);
        }
    }
}