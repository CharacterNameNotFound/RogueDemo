using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Fire)]
    public class FireItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Fire)]
        public float Fire { get; set; }

        public FireItemLookUpEntry(string itemId, float fire)
        {
            ItemId = itemId;
            Fire = fire;
        }
        
    }
}