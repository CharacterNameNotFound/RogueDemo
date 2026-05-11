using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets
{
    // ToDo rename?
    [Serializable]
    public class ItemStatEntry
    {
        public StatSet ItemValues; // item values
        public StatSet ItemPercentiles; // percentile modifiers

        public ItemStatEntry(StatSet itemValues, StatSet itemPercentiles)
        {
            ItemValues = itemValues;
            ItemPercentiles = itemPercentiles;
        }

        public ItemStatEntry GetCopy()
        {
            return new ItemStatEntry(ItemValues.GetCopy(), ItemPercentiles.GetCopy());
        }
    }
}