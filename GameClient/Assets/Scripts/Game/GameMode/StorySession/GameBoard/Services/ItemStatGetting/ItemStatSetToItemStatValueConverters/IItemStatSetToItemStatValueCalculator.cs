using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters
{
    public interface IItemStatSetToItemStatValueCalculator
    {
        public ItemStatType ProcessedStat { get; }
        public float GetValue(Item item, StatSet.StatSetComponent baseCalculateDepth, StatSet.StatSetComponent multiplicationCalculateDepth);
    }
}