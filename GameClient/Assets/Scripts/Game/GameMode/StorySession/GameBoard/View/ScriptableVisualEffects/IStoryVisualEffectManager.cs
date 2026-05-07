using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects
{
    public interface IStoryVisualEffectManager
    {
        public UniTask Initialize(CancellationToken cancellationToken);

        public UniTask PlayFlyingText(
            Transform parent, 
            Vector3 startingPosition, 
            Vector3 destinationPosition,
            string text, 
            float fontSize,
            CancellationToken cancellationToken);

        public void Clear();
    }
}