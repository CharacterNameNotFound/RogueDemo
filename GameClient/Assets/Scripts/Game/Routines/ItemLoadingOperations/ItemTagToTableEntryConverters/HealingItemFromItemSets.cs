using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class HealingItemFromItemSets : IItemTagToTableEntryConverter
    {
        private IItemStatGetter _itemStatGetter;

        public HealingItemFromItemSets(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }
        
        public bool TryGetInsertObject(Item item, out object insert)
        {
            if (!item.Tags.TagsList.Contains(ItemTag.Healing))
            {
                insert = null;
                return false;
            }
            
            float healValue = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.Heal, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);

            insert = new HealingItemLookUpEntry(item.ItemId, healValue);
            
            return true;
        }
    }
}