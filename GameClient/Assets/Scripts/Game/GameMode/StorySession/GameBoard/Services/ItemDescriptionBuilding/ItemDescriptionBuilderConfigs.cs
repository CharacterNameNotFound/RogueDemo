using GameWideSystems.LocalizationWrapper;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemDescriptionBuilding
{
    public class ItemDescriptionBuilderConfigs : ScriptableObject
    {
        public LocalizedLineKey ItemRarityKey = new("item_rarity", TranslationCategory.ItemDescription);
        public LocalizedLineKey ItemStats = new("item_stats", TranslationCategory.ItemDescription);
        public LocalizedLineKey ItemEffectsKey = new("item_effects", TranslationCategory.ItemDescription);
        public LocalizedLineKey ItemTagsKey = new("item_tags", TranslationCategory.ItemDescription);

        public LocalizedLineKey BronzeRarityKey = new("bronze_item_rarity", TranslationCategory.ItemDescription);
        public LocalizedLineKey SilverRarityKey = new("silver_item_rarity", TranslationCategory.ItemDescription);
        public LocalizedLineKey GoldRarityKey = new("gold_item_rarity", TranslationCategory.ItemDescription);
        public LocalizedLineKey DiamondRarityKey = new("diamond_item_rarity", TranslationCategory.ItemDescription);
        public LocalizedLineKey LegendaryRarityKey = new("legendary_item_rarity", TranslationCategory.ItemDescription);

        public TranslationCategory ItemTagCategory = TranslationCategory.ItemDescription;
        public string ItemStatSuffix = "_stat";
        public string ItemTagSuffix = "_tag";

    }
}