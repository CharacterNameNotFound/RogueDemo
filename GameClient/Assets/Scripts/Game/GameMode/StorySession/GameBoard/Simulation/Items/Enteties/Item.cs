using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.Tags;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties
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

        public Item()
        {
            Triggers = new();
            ItemStats = new();
        }
        
        private Item(List<Trigger> triggers)
        {
            Triggers = triggers;
        }

        public Item GetCopy()
        {
            List<Trigger> triggers = new();

            foreach (Trigger trigger in Triggers)
            {
                triggers.Add(trigger.GetCopy());
            }
            
            Item item = new Item(triggers);

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

            return item;
        }
    }
    
}