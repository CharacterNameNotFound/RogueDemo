using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.ItemBasic)]
    public class BasicsItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.ItemSize)]
        public int Size { get; set; }
        
        [Column(ColumnNames.IsNeutral)]
        public bool IsNeutral { get; set; }

        public BasicsItemLookUpEntry(string itemId, int size, bool isNeutral)
        {
            ItemId = itemId;
            Size = size;
            IsNeutral = isNeutral;
        }
        
    }
}