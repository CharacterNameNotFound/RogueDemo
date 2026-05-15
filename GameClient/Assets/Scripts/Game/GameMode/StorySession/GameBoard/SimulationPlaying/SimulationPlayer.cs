using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Utilities.EventArguments;
using Utils.UtilityTypes.EventProcessing;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public class SimulationPlayer : ISimulationPlayer
    {
        private ISimulationLoop _simulationLoop;
        
        private IEventDispatcher<PreFightArguments> _preFightEventDispatcher;
        private IEventDispatcher<PostFightArguments> _postFightEventDispatcher;

        public SimulationPlayer(
            ISimulationLoop simulationLoop, 
            IEventDispatcher<PreFightArguments> preFightEventDispatcher, 
            IEventDispatcher<PostFightArguments> postFightEventDispatcher)
        {
            _simulationLoop = simulationLoop;
            _preFightEventDispatcher = preFightEventDispatcher;
            _postFightEventDispatcher = postFightEventDispatcher;
        }


        public async UniTask<bool> PlaySimulation(CancellationToken cancellationToken)
        {
            await _preFightEventDispatcher.Invoke(new PreFightArguments(), cancellationToken);
            
            await _simulationLoop.PrepareSimulationEnvironment(cancellationToken);

            await _simulationLoop.Loop(cancellationToken);

            await _simulationLoop.PostSimulationCleanUp(cancellationToken);
            
            await _postFightEventDispatcher.Invoke(new PostFightArguments(), cancellationToken);
            
            return true;
        }
        
        
        
    }
}