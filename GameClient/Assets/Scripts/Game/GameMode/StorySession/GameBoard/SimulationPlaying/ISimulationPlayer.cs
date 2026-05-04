using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public interface ISimulationPlayer
    {
        public UniTask<bool> PlaySimulation(CancellationToken cancellationToken);
    }
}