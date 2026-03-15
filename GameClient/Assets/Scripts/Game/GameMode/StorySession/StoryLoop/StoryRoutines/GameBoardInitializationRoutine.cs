using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.LifeCycle;

namespace Game.GameMode.StorySession.StoryLoop.StoryRoutines
{
    public class GameBoardInitializationRoutine
    {
        private StoryInitializationAddressableProvider _storyInitializationAddressableProvider;
        private GameBoardHolder _gameBoardHolder;

        public GameBoardInitializationRoutine(StoryInitializationAddressableProvider storyInitializationAddressableProvider, GameBoardHolder gameBoardHolder)
        {
            _storyInitializationAddressableProvider = storyInitializationAddressableProvider;
            _gameBoardHolder = gameBoardHolder;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            _gameBoardHolder.Initialize();
            
            GameBoardComponent gameBoardComponent = await _storyInitializationAddressableProvider.GameBoardPrefab
                .Instantiate<GameBoardComponent>(new InstantiationParameters(_gameBoardHolder.GameHolder, true), cancellationToken);
            
            _gameBoardHolder.GameBoardComponent = gameBoardComponent;

            IInitializableGameObject[] initializables = gameBoardComponent.GetComponentsInChildren<IInitializableGameObject>(true);

            foreach (IInitializableGameObject item in initializables)
            {
                await item.Initialize(cancellationToken);
            }
            
        }
        
    }
}