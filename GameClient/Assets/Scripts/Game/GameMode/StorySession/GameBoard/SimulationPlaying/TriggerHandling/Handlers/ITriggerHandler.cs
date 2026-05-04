using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling.Handlers
{
    public interface ITriggerHandler
    {
        public Type HandledTriggerType { get; }
        public UniTask HandleTrigger(TriggerToken triggerToken, BattleCache battleCache, CancellationToken cancellationToken);
    }
}