using System;
using System.Collections.Generic;
using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Localization;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.Tags;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;
using GameWideSystems.LocalizationWrapper;
using Mono.CSharp;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemDescriptionBuilding
{
    public class ItemDescriptionBuilder : IItemDescriptionBuilder
    {
        private ItemDescriptionBuilderConfigs _descriptionBuilderConfigs;
        private ItemDescriptionLocalizationConfigs _itemLocalizationConfigs;
        private ILocalizationManager _localizationManager;
        private IItemStatGetter _iItemStatGetter;

        public ItemDescriptionBuilder(
            ItemDescriptionBuilderConfigs descriptionBuilderConfigs, 
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs, 
            ILocalizationManager localizationManager, 
            IItemStatGetter iItemStatGetter)
        {
            _descriptionBuilderConfigs = descriptionBuilderConfigs;
            _itemLocalizationConfigs = itemLocalizationConfigs;
            _localizationManager = localizationManager;
            _iItemStatGetter = iItemStatGetter;
        }

        public string GetItemName(Item item)
        {
            _localizationManager.TryGetLocalized(item.ItemName, TranslationCategory.Items, out string itemName);
            return itemName;
        }

        public string GetItemDescription(Item item)
        {
            StringBuilder itemDescription = new StringBuilder();

            string temp;
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemRarityKey, out temp);
            itemDescription.AppendLine($"{temp}: {RarityToLine(item.ItemRarity)}");
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemStats, out temp);
            itemDescription.AppendLine(temp);
            CollectStats(item, itemDescription);
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemEffectsKey, out temp);
            itemDescription.AppendLine(temp);
            
            // Collect effects
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemTagsKey, out temp);
            itemDescription.Append($"{temp}: ");
            AppendTags(item, itemDescription);

            
            return itemDescription.ToString();
        }

        private void CollectStats(Item item, StringBuilder itemDescription)
        {
            foreach (KeyValuePair<ItemStatType, ItemStatEntry> stat in item.ItemStats.Stats)
            {
                itemDescription.Append('\t');
                _localizationManager.TryGetLocalized(
                    $"{stat.Key.ToString()}{_descriptionBuilderConfigs.ItemStatSuffix}", 
                    _descriptionBuilderConfigs.ItemTagCategory,
                    out string line);

                float statValue = _iItemStatGetter.GetStatValue(
                    item, 
                    stat.Key, 
                    StatSet.StatSetComponent.Special, 
                    StatSet.StatSetComponent.Special);
                
                itemDescription.AppendLine($"{line}: {statValue}");
            }
            
        }

        private void AppendTags(Item item, StringBuilder itemDescription)
        {
            foreach (ItemTag tag in item.Tags.TagsList)
            {
                _localizationManager.TryGetLocalized(
                    $"{tag.ToString()}{_descriptionBuilderConfigs.ItemTagSuffix}", 
                    _descriptionBuilderConfigs.ItemTagCategory,
                    out string line);

                itemDescription.Append($"{line},");
            }

            itemDescription.Remove(itemDescription.Length - 1, 1);
        }


        private string RarityToLine(ItemRarity itemRarity)
        {
            return itemRarity switch
            {
                ItemRarity.Bronze => _localizationManager.GetLocalized(_descriptionBuilderConfigs.BronzeRarityKey),
                ItemRarity.Silver => _localizationManager.GetLocalized(_descriptionBuilderConfigs.SilverRarityKey),
                ItemRarity.Gold => _localizationManager.GetLocalized(_descriptionBuilderConfigs.GoldRarityKey),
                ItemRarity.Diamond => _localizationManager.GetLocalized(_descriptionBuilderConfigs.DiamondRarityKey),
                ItemRarity.Legendary => _localizationManager.GetLocalized(_descriptionBuilderConfigs.LegendaryRarityKey),
                _ => throw new ArgumentOutOfRangeException(nameof(itemRarity), itemRarity, null)
            };
        }
        
    }
}