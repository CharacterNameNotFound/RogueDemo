using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;

namespace Game.GameMode.StorySession.GameBoard.Services.GameBoardManagement
{
    public class ItemBoardModelUpdater : IItemBoardModelUpdater
    {
        public void RemoveItem(int position, ItemBoardModel itemBoardModel)
        {
            Item item = itemBoardModel.Items[position];

            for (int i = 0; i < item.ItemSize; i++)
            {
                itemBoardModel.Items[position + i] = null;
            }
            
            // ToDo: notify item removed
        }

        public void PlaceItem(Item item, int position, ItemBoardModel itemBoardModel)
        {
            for (int i = 0; i < item.ItemSize; i++)
            {
                itemBoardModel.Items[position + i] = item;
            }
            
            // ToDo: notify item placed
        }
        
    }
}