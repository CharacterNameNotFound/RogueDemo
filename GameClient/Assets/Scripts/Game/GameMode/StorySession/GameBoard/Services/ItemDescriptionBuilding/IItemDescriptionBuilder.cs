using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemDescriptionBuilding
{
    public interface IItemDescriptionBuilder
    {
        public string GetItemName(Item item);
        public string GetItemDescription(Item item);
    }
}