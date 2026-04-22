using Game.GameMode.StorySession.GameBoard.Simulation;

namespace Game.GameMode.StorySession.GameBoard.Services.GameBoardManagement
{
    public interface IItemBoardModelUpdater
    {
        public void RemoveItem(int position, ItemBoardModel itemBoardModel);
    }
}