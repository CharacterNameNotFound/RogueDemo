using Game.GameMode.StorySession.GameBoard.Configurations;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Models
{
    public class ItemBoardModel
    {
        public Item[] Items = new Item[ItemConfigurations.ItemCapacity];
    }
}