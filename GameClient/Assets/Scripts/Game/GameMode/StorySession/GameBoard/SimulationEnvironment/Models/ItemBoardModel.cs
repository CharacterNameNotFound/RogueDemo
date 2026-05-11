using Game.GameMode.StorySession.GameBoard.Configurations;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models
{
    public class ItemBoardModel
    {
        public Item[] Items = new Item[ItemConfigurations.ItemCapacity];
    }
}