using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Weapon)]
    public class WeaponItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Damage)]
        public float Damage { get; set; }

        public WeaponItemLookUpEntry(string itemId, float damage)
        {
            ItemId = itemId;
            Damage = damage;
        }
    }
}