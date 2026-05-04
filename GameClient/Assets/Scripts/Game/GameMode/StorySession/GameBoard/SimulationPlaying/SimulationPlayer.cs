using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public class SimulationPlayer : ISimulationPlayer
    {
        private ISimulationLoop _simulationLoop;

        public SimulationPlayer(ISimulationLoop simulationLoop)
        {
            _simulationLoop = simulationLoop;
        }


        public async UniTask<bool> PlaySimulation(CancellationToken cancellationToken)
        {
            await _simulationLoop.PrepareSimulationEnvironment(cancellationToken);

            await _simulationLoop.Loop(cancellationToken);
            
            
            
            
            
            return true;
        }
        
        
        
    }
}