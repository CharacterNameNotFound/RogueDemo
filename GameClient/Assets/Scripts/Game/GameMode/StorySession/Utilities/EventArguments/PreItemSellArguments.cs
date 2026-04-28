using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.Utilities.EventArguments
{
    public class PreItemSellArguments
    {
        public ItemContainerComponent Item;
        
        public PreItemSellArguments(ItemContainerComponent item)
        {
            Item = item;
        }
    }
}