using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils
{
    public interface IPlayFlyingTextShortcuts
    {
        public UniTask PlayFlyingTextAtItemPosition(
            int itemIndex,
            int itemOwner,
            Vector3 movementTrajectory,
            string text, 
            bool isCrit,
            CancellationToken cancellationToken);

        public UniTask PlayFlyingTextAtPosition(
            Transform parent, 
            Vector3 startingPosition, 
            Vector3 destinationPosition,
            string text, 
            CancellationToken cancellationToken);
        
        
        
    }
}