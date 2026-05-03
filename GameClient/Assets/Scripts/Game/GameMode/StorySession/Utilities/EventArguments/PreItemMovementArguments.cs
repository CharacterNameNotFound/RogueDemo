using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.Utilities.EventArguments
{
    public class PreItemMovementArguments
    {
        public ItemContainerComponent Item;
        public ItemLineComponent OriginalLine;
        public int OriginalIndex;

        public PreItemMovementArguments(
            ItemContainerComponent item, 
            ItemLineComponent originalLine, 
            int originalIndex)
        {
            Item = item;
            OriginalLine = originalLine;
            OriginalIndex = originalIndex;
        }
    }
}