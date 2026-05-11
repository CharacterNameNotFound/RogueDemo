using System;
using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities
{
    public class Item
    {
        public string ItemId;
        public string ItemSetId;
        public string ItemName;
        public ItemRarity ItemRarity;
        public int ItemSize = 1;
        public string UpgradedItemId;
        public string DowngradedItemId;
        public object ItemSpriteRuntimeKey;
        
        public TagList Tags;
        public List<Trigger> Triggers;
        public ItemStatSet ItemStats;

        // used for scripting
        public bool IsNonDeck;
        
        //this is overkill, but let it be
        public Dictionary<Type, IItemStatusEffect> StatusEffects;
        
        
        public Item()
        {
            Triggers = new();
            ItemStats = new();
            StatusEffects = new();
        }
        
        private Item(List<Trigger> triggers)
        {
            Triggers = triggers;
        }

        public Item GetCopy()
        {
            // ToDo: clean-up
            Item item = new Item(Triggers.Select(x => x.GetCopy()).ToList());

            item.ItemId = ItemId;
            item.ItemSetId = ItemSetId;
            item.ItemName = ItemName;
            item.ItemRarity = ItemRarity;
            item.ItemSize = ItemSize;
            item.UpgradedItemId = UpgradedItemId;
            item.DowngradedItemId = DowngradedItemId;
            item.ItemSpriteRuntimeKey = ItemSpriteRuntimeKey;
            
            item.Tags = Tags.GetCopy();
            item.ItemStats = ItemStats.GetCopy();

            item.IsNonDeck = IsNonDeck;
            item.StatusEffects = StatusEffects.Values.Select(x => x.GetCopy()).ToDictionary(x=> x.GetType());

            return item;
        }
        
    }
    
}