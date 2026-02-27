using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.Tags;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items
{
    public class Item
    {
        public TagList Tags;
        public List<Trigger> Triggers;
        public ItemStatSet ItemStats;

        public Item()
        {
            Triggers = new();
        }
    }
    
}