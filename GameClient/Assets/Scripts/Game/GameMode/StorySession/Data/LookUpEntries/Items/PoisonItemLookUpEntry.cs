using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Poison)]
    public class PoisonItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Poison)]
        public float Poison { get; set; }

        public PoisonItemLookUpEntry(string itemId, float poison)
        {
            ItemId = itemId;
            Poison = poison;
        }
    }
}