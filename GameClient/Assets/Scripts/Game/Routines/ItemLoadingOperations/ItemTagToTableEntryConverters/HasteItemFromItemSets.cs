using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class HasteItemFromItemSets : IItemTagToTableEntryConverter
    {
        private IItemStatGetter _itemStatGetter;

        public HasteItemFromItemSets(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }
        
        public bool TryGetInsertObject(Item item, out object insert)
        {
            if (!item.Tags.TagsList.Contains(ItemTag.Haste))
            {
                insert = null;
                return false;
            }
            
            float hasteDuration = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.HasteDuration, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);
            
            float hasteTargetCount = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.HasteTargetCount, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);

            insert = new HasteItemLookUpEntry(item.ItemId, hasteDuration, hasteTargetCount);
            
            return true;
        }
    }
}