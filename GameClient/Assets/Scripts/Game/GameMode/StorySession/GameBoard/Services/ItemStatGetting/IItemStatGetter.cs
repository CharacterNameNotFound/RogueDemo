using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting
{
    public interface IItemStatGetter
    {
        public float GetStatValue(
            Item item, 
            ItemStatType itemStat, 
            StatSet.StatSetComponent baseCalculateDepth = StatSet.StatSetComponent.Special, 
            StatSet.StatSetComponent multiplicationCalculateDepth = StatSet.StatSetComponent.Special);
    }
}