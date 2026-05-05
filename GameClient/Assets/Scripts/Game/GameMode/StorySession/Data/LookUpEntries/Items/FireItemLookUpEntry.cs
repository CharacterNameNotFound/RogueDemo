using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Burn)]
    public class FireItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Burn)]
        public float Burn { get; set; }

        public FireItemLookUpEntry(string itemId, float burn)
        {
            ItemId = itemId;
            Burn = burn;
        }
        
    }
}