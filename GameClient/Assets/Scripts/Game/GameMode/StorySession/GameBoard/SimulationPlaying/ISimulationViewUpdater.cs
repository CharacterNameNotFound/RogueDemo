using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public interface ISimulationViewUpdater
    {
        void RenderChargeValues(List<ItemCache> playerItems, List<ItemCache> encounterItems);
    }
}