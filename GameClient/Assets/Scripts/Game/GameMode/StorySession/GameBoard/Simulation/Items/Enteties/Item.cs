using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.Tags;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;


namespace Game.GameMode.StorySession.GameBoard.Simulation.Items
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
        
    }
    
}