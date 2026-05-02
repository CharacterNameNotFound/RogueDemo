using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.Utilities.EventArguments
{
    public class PreItemPurchaseArguments
    {
        public ItemContainerComponent ItemContainerComponent;

        public PreItemPurchaseArguments(ItemContainerComponent itemContainerComponent)
        {
            ItemContainerComponent = itemContainerComponent;
        }
    }
}