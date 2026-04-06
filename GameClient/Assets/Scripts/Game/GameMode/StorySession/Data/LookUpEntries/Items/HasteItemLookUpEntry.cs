using Game.Utilities.Constants.LookUpTables;
using SQLite;

namespace Game.GameMode.StorySession.Data.LookUpEntries.Items
{
    [Table(ItemTableNames.Haste)]
    public class HasteItemLookUpEntry
    {
        [PrimaryKey][Column(ColumnNames.Id)] 
        public string ItemId { get; set; }
        
        [Column(ColumnNames.Duration)]
        public float Duration { get; set; }
        
        [Column(ColumnNames.TargetCount)]
        public float DurationTargetCount { get; set; }

        public HasteItemLookUpEntry(string itemId, float duration, float durationTargetCount)
        {
            ItemId = itemId;
            Duration = duration;
            DurationTargetCount = durationTargetCount;
        }
    }
}