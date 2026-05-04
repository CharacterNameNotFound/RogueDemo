using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public interface ISimulationLoop
    {
        public UniTask PrepareSimulationEnvironment(CancellationToken cancellationToken);
        public UniTask Loop(CancellationToken cancellationToken);
        public UniTask PostSimulationCleanUp(CancellationToken cancellationToken);
    }
}