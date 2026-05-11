using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class WeaponItemFromItemSets : IItemTagToTableEntryConverter
    {

        private IItemStatGetter _itemStatGetter;

        public WeaponItemFromItemSets(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }

        public bool TryGetInsertObject(Item item, out object insert)
        {
            if (!item.Tags.TagsList.Contains(ItemTag.Weapon))
            {
                insert = null;
                return false;
            }
            
            float damageValue = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.Damage, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);

            insert = new WeaponItemLookUpEntry(item.ItemId, damageValue);
            
            return true;
        }
        
    }
}