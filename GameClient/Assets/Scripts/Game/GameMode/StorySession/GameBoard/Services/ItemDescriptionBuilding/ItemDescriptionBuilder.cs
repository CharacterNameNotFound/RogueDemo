using System;
using System.Collections.Generic;
using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemDescriptionBuilding
{
    public class ItemDescriptionBuilder : IItemDescriptionBuilder
    {
        private ItemDescriptionBuilderConfigs _descriptionBuilderConfigs;
        private ItemDescriptionLocalizationConfigs _itemLocalizationConfigs;
        private ILocalizationManager _localizationManager;
        private IItemStatGetter _itemStatGetter;

        public ItemDescriptionBuilder(
            ItemDescriptionBuilderConfigs descriptionBuilderConfigs, 
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs, 
            ILocalizationManager localizationManager, 
            IItemStatGetter itemStatGetter)
        {
            _descriptionBuilderConfigs = descriptionBuilderConfigs;
            _itemLocalizationConfigs = itemLocalizationConfigs;
            _localizationManager = localizationManager;
            _itemStatGetter = itemStatGetter;
        }

        public string GetItemName(Item item)
        {
            _localizationManager.TryGetLocalized(item.ItemName, TranslationCategory.Items, out string itemName);
            return itemName;
        }

        // ToDo: not sure about how battle execution will look, so this solution could be temporary
        // It may make sense to pre build all localization lines on conversion, then to have stats provided as localization substitutes, but that requires prebuilding all languages...
        // and denies ability to change how item works runtime
        // It is possible to build system, that will be doubling all item components to build localization lines, but that certainly will add work down the line
        // Current way certainly easies, and around fastest after pregenerating.
        public string GetItemDescription(Item item)
        {
            StringBuilder itemDescription = new StringBuilder();

            string temp;
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemRarityKey, out temp);
            itemDescription.AppendLine($"{temp}: {RarityToLine(item.ItemRarity)}");
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemStats, out temp);
            itemDescription.AppendLine($"{temp}:");
            CollectStats(item, itemDescription);
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemEffectsKey, out temp);
            itemDescription.AppendLine($"{temp}:");
            CollectEffects(item, itemDescription);
            
            _localizationManager.TryGetLocalized(_descriptionBuilderConfigs.ItemTagsKey, out temp);
            itemDescription.AppendLine($"{temp}:");
            AppendTags(item, itemDescription);

            AppendStatusEffects(item, itemDescription);
            
            return itemDescription.ToString();
        }

        private void CollectStats(Item item, StringBuilder itemDescription)
        {
            itemDescription.Append($"<margin-left={_itemLocalizationConfigs.MarginSize}>");
            
            foreach (KeyValuePair<ItemStatType, ItemStatEntry> stat in item.ItemStats.Stats)
            {
                if (_descriptionBuilderConfigs.SkippedItemStats.Contains(stat.Key))
                {
                    continue;
                }
                
                _localizationManager.TryGetLocalized(
                    _itemLocalizationConfigs.ItemStatTypeToLocalizedLineKey(stat.Key), 
                    out string line);

                float statValue = _itemStatGetter.GetStatValue(item, stat.Key);
                
                itemDescription.AppendLine($"{line}: {statValue}");
            }
            
            itemDescription.Append($"</margin>");
        }
        
        private void CollectEffects(Item item, StringBuilder itemDescription)
        {
            foreach (Trigger itemTrigger in item.Triggers)
            {
                itemTrigger.AppendDescription(1, item, itemDescription, _itemStatGetter, _localizationManager, _itemLocalizationConfigs);
            }
            
            itemDescription.Append($"</margin>");
        }

        private void AppendTags(Item item, StringBuilder itemDescription)
        {
            itemDescription.Append($"<margin-left={_itemLocalizationConfigs.MarginSize}>");

            foreach (ItemTag tag in item.Tags.TagsList)
            {
                _localizationManager.TryGetLocalized(
                    _itemLocalizationConfigs.ItemTagToLocalizationKey(tag),
                    out string line);

                itemDescription.Append($" {line},");
            }

            itemDescription.Remove(itemDescription.Length - 1, 1);
            itemDescription.Append($"</margin>");

        }

        private void AppendStatusEffects(Item item, StringBuilder itemDescription)
        {
            if (item.StatusEffects.Count == 0)
            {
                return;
            }

            itemDescription.AppendLine();
            itemDescription.AppendLine(_localizationManager.GetLocalized(_descriptionBuilderConfigs.ItemStatusEffectsKey));
            
            itemDescription.Append($"<margin-left={_itemLocalizationConfigs.MarginSize}>");

            foreach (IItemStatusEffect statusEffect in item.StatusEffects.Values)
            {
                statusEffect.AppendDescription(
                    1, 
                    item, 
                    itemDescription, 
                    _localizationManager,
                    _itemLocalizationConfigs);
            }
            
            itemDescription.Append($"</margin>");

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