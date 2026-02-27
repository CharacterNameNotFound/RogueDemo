using System;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets
{
    [Serializable]
    public class ItemStatEntry
    {
        public StatSet ItemValues; // item values
        public StatSet ItemPercentiles; // percentile modifiers
    }
}