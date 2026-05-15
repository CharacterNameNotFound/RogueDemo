using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils.FlyingParticle
{
    public interface IFlyingParticleShortcuts
    {
        public UniTask PlayHasteParticle(
            int itemOriginIndex,
            int itemOriginOwner,
            int itemDestinationIndex,
            int itemDestinationOwner,
            CancellationToken cancellationToken);

        public UniTask PlaySlowParticle(
            int itemOriginIndex,
            int itemOriginOwner,
            int itemDestinationIndex,
            int itemDestinationOwner,
            CancellationToken cancellationToken);


    }
}