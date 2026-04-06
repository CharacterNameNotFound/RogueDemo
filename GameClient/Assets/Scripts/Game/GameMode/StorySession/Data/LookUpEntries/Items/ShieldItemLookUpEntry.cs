using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Shield)]
    public class ShieldItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Shield)]
        public float Shield { get; set; }

        public ShieldItemLookUpEntry(string itemId, float shield)
        {
            ItemId = itemId;
            Shield = shield;
        }
        
    }
}