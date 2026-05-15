using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatusEffects;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects.ItemStatusEffectVFXApplication
{
    public interface IItemStatusEffectVFXApplier
    {
        public UniTask ApplyItemFrameParticles<T>(
            Func<int, int, CancellationToken, UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)>> effectGetter, 
            int itemIndex,
            int ownerIndex, 
            CancellationToken cancellationToken) where T : IItemStatusEffect;

        public void RemoveItemFrameParticles(Type type, int itemIndex, int ownerIndex);


    }
}