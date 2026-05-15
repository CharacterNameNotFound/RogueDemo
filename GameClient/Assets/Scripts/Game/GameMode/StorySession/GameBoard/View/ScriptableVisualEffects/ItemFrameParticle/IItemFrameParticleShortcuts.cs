using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle
{
    public interface IItemFrameParticleShortcuts
    {
        public UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)> GetHasteParticles(int index, int owner, CancellationToken cancellationToken);
        public UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)> GetSlowParticles(int index, int owner, CancellationToken cancellationToken);
    }
}