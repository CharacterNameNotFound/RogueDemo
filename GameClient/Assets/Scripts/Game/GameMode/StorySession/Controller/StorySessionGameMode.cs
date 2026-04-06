using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory;
using Game.GameMode.StorySession.UI;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.GameStateManagement;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.UIManagerRequests;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Zenject;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameMode : IGameStateController
    {
        
        public class Factory : PlaceholderFactory<StorySessionGameMode> { }
        

        private StorySessionGameModeInitializationParameters _gmParams;
        private IStoryBase _story;
        private CharacterData _characterData;

        public StorySessionGameMode()
        {

        }

        public async UniTask Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken = default)
        {
            _gmParams = (StorySessionGameModeInitializationParameters) parameters;
            _story = await _gmParams.StoryStartData.StoryID.Load<IStoryBase>(cancellationToken);
            
            ProjectContext.Instance.Container.Inject(_story);
            
            _characterData = await _gmParams.StoryStartData.CharacterID.Load<CharacterData>(cancellationToken); 
            await _story.Initialize(new BaseStoryInitializationData(_characterData), cancellationToken);
        }

        public UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default)
        {
            _story.StartStory(cancellationToken).Forget();
            return UniTask.CompletedTask;
        }

        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Unload(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Close(CancellationToken cancellationToken = default)
        {
            Addressables.Release(_characterData);
            return UniTask.CompletedTask;
        }

        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData,
            CancellationToken cancellationToken = default)
        {
            gameStateSerializationData = null;
            return UniTask.FromResult(true);
        }
    }
}