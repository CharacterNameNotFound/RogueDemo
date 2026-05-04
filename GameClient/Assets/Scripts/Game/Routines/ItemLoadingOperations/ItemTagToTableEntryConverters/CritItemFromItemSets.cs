using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public class CritItemFromItemSets : IItemTagToTableEntryConverter
    {
        private IItemStatGetter _itemStatGetter;

        public CritItemFromItemSets(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }

        public bool TryGetInsertObject(Item item, out object insert)
        {
            if (!item.Tags.TagsList.Contains(ItemTag.Critical))
            {
                insert = null;
                return false;
            }
            
            float critChance = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.CriticalChance, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);
            
            float critDamage = _itemStatGetter.GetStatValue(
                item, 
                ItemStatType.CriticalChance, 
                StatSet.StatSetComponent.BaseValue,
                StatSet.StatSetComponent.None);

            insert = new CriticalItemLookUpEntry(item.ItemId, critChance, critDamage);
            
            return true;
        }
        
        
    }
}