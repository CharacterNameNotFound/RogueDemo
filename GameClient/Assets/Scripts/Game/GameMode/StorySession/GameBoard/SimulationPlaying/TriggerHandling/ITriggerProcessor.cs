using System.Threading;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling
{
    public interface ITriggerProcessor
    {
        public void SetCache(BattleCache battleCache);
        public void Process(TriggerBuffer triggerBuffer, CancellationToken cancellationToken);
    }
}