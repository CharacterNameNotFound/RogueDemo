using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class SizeItemTableConverter : IItemTagToTableEntryConverter
    {
        
        public bool TryGetInsertObject(Item item, out object insert)
        {
            insert = new BasicsItemLookUpEntry(item.ItemId, item.ItemSize, item.ItemSetId is null, (int) item.ItemRarity);

            return true;
        }
    }
}