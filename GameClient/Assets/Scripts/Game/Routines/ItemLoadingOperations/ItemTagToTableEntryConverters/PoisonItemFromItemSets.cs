using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class PoisonItemFromItemSets : IItemTagToTableEntryConverter
    {
        private IItemStatGetter _itemStatGetter;

        public PoisonItemFromItemSets(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }
        
        public bool TryGetInsertObject(Item item, out object insert)
        {
            if (!item.Tags.TagsList.Contains(ItemTag.Poison))
            {
                insert = null;
                return false;
            }
            
            float poisonValue = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.Poison, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);

            insert = new PoisonItemLookUpEntry(item.ItemId, poisonValue);
            
            return true;
        }
    }
}