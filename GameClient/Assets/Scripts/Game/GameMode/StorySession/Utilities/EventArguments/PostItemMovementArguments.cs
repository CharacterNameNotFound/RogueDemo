using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.Utilities.EventArguments
{
    public class PostItemMovementArguments
    {
        public ItemContainerComponent Item;
        
        public PostItemMovementArguments(ItemContainerComponent item)
        {
            Item = item;
        }
    }
}