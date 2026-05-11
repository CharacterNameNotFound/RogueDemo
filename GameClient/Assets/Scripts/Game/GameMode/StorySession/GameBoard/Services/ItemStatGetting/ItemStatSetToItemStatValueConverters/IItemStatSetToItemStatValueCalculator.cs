using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters
{
    public interface IItemStatSetToItemStatValueCalculator
    {
        public ItemStatType ProcessedStat { get; }
        public float GetValue(Item item, StatSet.StatSetComponent baseCalculateDepth, StatSet.StatSetComponent multiplicationCalculateDepth);

        public float GetValue(
            ItemStatEntry statEntry, 
            StatSet.StatSetComponent baseCalculateDepth,
            StatSet.StatSetComponent multiplicationCalculateDepth);
        
    }
}