using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Regeneration)]
    public class RegenerationItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Regeneration)]
        public float Regeneration { get; set; }

        public RegenerationItemLookUpEntry(string itemId, float regeneration)
        {
            ItemId = itemId;
            Regeneration = regeneration;
        }
    }
}