using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.Utilities.EventArguments
{
    public class PreItemMovementArguments
    {
        public ItemContainerComponent Item;

        public PreItemMovementArguments(ItemContainerComponent item)
        {
            Item = item;
        }
    }
}