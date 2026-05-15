using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect;
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

        public UniTask PlayFlyingParticleVFX(
            FlyingParticleParameters flyingParticleParameters,
            CancellationToken cancellationToken);

        public UniTask<ItemFrameParticles> GetItemFameParticleSystem(
            ItemFrameParticlesParameters itemFrameParticlesParameters, 
            CancellationToken cancellationToken);

        public void Clear();
    }
}