using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public interface ISimulationModelUpdater
    {
        public void ResetChargeValues(List<ItemCache> items);
        public void ProgressCharge(List<ItemCache> items, TriggerBuffer triggerBuffer, float deltaTime);
    }
}