using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class SlowItemFromItemSets : IItemTagToTableEntryConverter
    {
        private IItemStatGetter _itemStatGetter;

        public SlowItemFromItemSets(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }
        
        public bool TryGetInsertObject(Item item, out object insert)
        {
            if (!item.Tags.TagsList.Contains(ItemTag.Slow))
            {
                insert = null;
                return false;
            }
            
            float slowDuration = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.SlowDuration, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);
            
            float slowTargetCount = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.SlowTargetCount, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);

            insert = new SlowItemLookUpEntry(item.ItemId, slowDuration, slowTargetCount);
            
            return true;
        }
    }
}