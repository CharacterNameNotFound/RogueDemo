using GameWideSystems.LocalizationWrapper;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization
{
    public class ItemDescriptionLocalizationConfigs : ScriptableObject
    {
        public int MarginSize = 2;
        
        // triggers
        public LocalizedLineKey OnChargeTrigger = new("on_charge_trigger", TranslationCategory.ItemDescription);
        
        
        // effectors
        public LocalizedLineKey DealDamage = new("deal_x_damage", TranslationCategory.ItemDescription);
        public LocalizedLineKey ApplyFire = new("apply_x_fire", TranslationCategory.ItemDescription);
        public LocalizedLineKey ApplyHaste = new("apply_haste", TranslationCategory.ItemDescription);
        public LocalizedLineKey ApplyHeal = new("apply_x_heal", TranslationCategory.ItemDescription);
        public LocalizedLineKey ApplyPoison = new("apply_x_poison", TranslationCategory.ItemDescription);
        public LocalizedLineKey ApplyRegeneration = new("apply_x_regeneration", TranslationCategory.ItemDescription);
        public LocalizedLineKey ApplyShield = new("apply_x_shield", TranslationCategory.ItemDescription);
        public LocalizedLineKey ApplySlow = new("apply_slow", TranslationCategory.ItemDescription);
        
        
        // target
        public LocalizedLineKey TargetSelf = new("target_self", TranslationCategory.ItemDescription);
        public LocalizedLineKey TargetSelfItems = new("target_self_items", TranslationCategory.ItemDescription);
        public LocalizedLineKey TargetSelfItemsAll = new("target_self_items_all", TranslationCategory.ItemDescription);
        public LocalizedLineKey TargetOpponent = new("target_opponent", TranslationCategory.ItemDescription);
        public LocalizedLineKey TargetOpponentItems = new("target_opponent_items", TranslationCategory.ItemDescription);
        public LocalizedLineKey TargetOpponentItemsAll = new("target_opponent_items_all", TranslationCategory.ItemDescription);
        
        // status effects
        public LocalizedLineKey HasteItemStatusEffect = new("haste_item_status_effect", TranslationCategory.ItemDescription);
        public LocalizedLineKey SlowItemStatusEffect = new("slow_item_status_effect", TranslationCategory.ItemDescription);
        
        
        
    }
}