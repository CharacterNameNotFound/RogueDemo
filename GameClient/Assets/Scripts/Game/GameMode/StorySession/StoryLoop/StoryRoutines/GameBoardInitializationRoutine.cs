using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory;
using GameWideSystems.LocalizationWrapper;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.LifeCycle;

namespace Game.GameMode.StorySession.StoryLoop.StoryRoutines
{
    public class GameBoardInitializationRoutine
    {
        private StoryInitializationAddressableProvider _storyInitializationAddressableProvider;
        private GameBoardHolder _gameBoardHolder;
        private ILocalizationManager _localizationManager;

        public GameBoardInitializationRoutine(
            StoryInitializationAddressableProvider storyInitializationAddressableProvider, 
            GameBoardHolder gameBoardHolder, 
            ILocalizationManager localizationManager)
        {
            _storyInitializationAddressableProvider = storyInitializationAddressableProvider;
            _gameBoardHolder = gameBoardHolder;
            _localizationManager = localizationManager;
        }

        public async UniTask Initialize(CharacterData characterData, CancellationToken cancellationToken)
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

            gameBoardComponent.PlayerBoard.Portrait.sprite = await characterData.CharacterStorySessionPortrait.Load<Sprite>(cancellationToken);

            foreach (LocalizeText localize in gameBoardComponent.GetComponentsInChildren<LocalizeText>())
            {
                await localize.Localize(_localizationManager, cancellationToken);
            }
            
            
        }

        public void CleanUp()
        {
            Addressables.Release(_gameBoardHolder.GameBoardComponent);
            Addressables.Release(_gameBoardHolder.GameBoardComponent.PlayerBoard.Portrait.sprite);
            
        }
        
    }
}