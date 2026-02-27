using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public class BaseStory : ScriptableObject, IStoryBase
    {
        private StoryInitializationAddressableProvider _storyInitializationAddressableProvider;
        private GameBoardHolder _gameBoardHolder;

        [Inject]
        private void InjectDependencies(
            StoryInitializationAddressableProvider storyInitializationAddressableProvider,
            GameBoardHolder gameBoardHolder
            )
        {
            _storyInitializationAddressableProvider = storyInitializationAddressableProvider;
            _gameBoardHolder = gameBoardHolder;
        }
        
        public async UniTask StartStory(CancellationToken cancellationToken)
        {
            _gameBoardHolder.Initialize();
            
            GameBoardComponent gameBoardComponent = await _storyInitializationAddressableProvider.GameBoardPrefab
                .Instantiate<GameBoardComponent>(new InstantiationParameters(_gameBoardHolder.GameHolder, true), cancellationToken);

            _gameBoardHolder.GameBoardComponent = gameBoardComponent;
        }

        public UniTask Load(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public UniTask Loop(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public UniTask Finish(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public UniTask CleanUp(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        
    }
}