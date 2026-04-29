using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Critical)]
    public class CriticalItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }

        [Column(ColumnNames.CritChance)]
        public float CritChance { get; set; }
        
        [Column(ColumnNames.CritDamage)]
        public float CritDamage { get; set; }
        
        public CriticalItemLookUpEntry(string itemId, float critChance, float critDamage)
        {
            ItemId = itemId;
            CritChance = critChance;
            CritDamage = critDamage;
        }
    }
}