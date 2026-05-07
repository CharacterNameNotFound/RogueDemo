using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.GameBoard.View.Utils;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils
{
    public class PlayFlyingTextShortcuts : IPlayFlyingTextShortcuts
    {
        private IStoryVisualEffectManager _storyVisualEffectManager;
        private GameBoardHolder _gameBoardHolder;
        private PlayFlyingTextShortcutsConfigs _playFlyingTextShortcutsConfigs;

        public PlayFlyingTextShortcuts(
            IStoryVisualEffectManager storyVisualEffectManager, 
            GameBoardHolder gameBoardHolder, 
            PlayFlyingTextShortcutsConfigs playFlyingTextShortcutsConfigs)
        {
            _storyVisualEffectManager = storyVisualEffectManager;
            _gameBoardHolder = gameBoardHolder;
            _playFlyingTextShortcutsConfigs = playFlyingTextShortcutsConfigs;
        }

        public UniTask PlayFlyingTextAtItemPosition(
            int itemIndex, 
            int itemOwner, 
            Vector3 movementTrajectory, 
            string text,
            bool isCrit,
            CancellationToken cancellationToken)
        {
            HeroGroup heroGroup = TargetCalculator.IndexToHeroGroup(itemOwner);

            ItemLineComponent itemLineComponent = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(heroGroup, _gameBoardHolder.GameBoardComponent);

            Vector3 startPosition = itemLineComponent.ItemContainerComponents[itemIndex].gameObject.transform.position;

            startPosition += (Vector3)(Random.insideUnitCircle * _playFlyingTextShortcutsConfigs.DispersionRadius);

            float fontSize = isCrit
                ? _playFlyingTextShortcutsConfigs.FontSizeOnCrit
                : _playFlyingTextShortcutsConfigs.FontSize;
            
            return _storyVisualEffectManager.PlayFlyingText(null, startPosition, startPosition + movementTrajectory, text, fontSize, cancellationToken);
        }

        public UniTask PlayFlyingTextAtPosition(
            Transform parent, 
            Vector3 startingPosition, 
            Vector3 destinationPosition, 
            string text,
            CancellationToken cancellationToken)
        {
            return _storyVisualEffectManager.PlayFlyingText(parent, startingPosition, destinationPosition, text, _playFlyingTextShortcutsConfigs.FontSize, cancellationToken);
        }

        
    }
}