using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class SizeItemTableConverter : IItemTagToTableEntryConverter
    {
        
        public bool TryGetInsertObject(Item item, out object insert)
        {
            insert = new BasicsItemLookUpEntry(item.ItemId, item.ItemSize, item.ItemSetId is null);

            return true;
        }
    }
}