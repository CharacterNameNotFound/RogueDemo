using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;

namespace Game.GameMode.StorySession.GameBoard.Services.BoardModelManipulation
{
    public interface IBoardModelManipulator
    {
        public void UpdatePlayerLines();
        public void UpdateLine(ItemBoardGroup group);
        public void AddItem(Item item, int index, ItemBoardGroup group);
        public void Remove(Item item, int index, ItemBoardGroup group);
        public void Clear(ItemBoardGroup group);
    }
}