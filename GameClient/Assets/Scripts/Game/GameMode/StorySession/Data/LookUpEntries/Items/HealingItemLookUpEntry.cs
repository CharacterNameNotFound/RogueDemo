using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Healing)]
    public class HealingItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Healing)]
        public float Healing { get; set; }

        public HealingItemLookUpEntry(string itemId, float healing)
        {
            ItemId = itemId;
            Healing = healing;
        }
    }
}