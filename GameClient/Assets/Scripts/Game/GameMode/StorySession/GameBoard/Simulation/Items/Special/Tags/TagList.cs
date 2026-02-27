using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.Tags
{
    [Serializable]
    public class TagList
    {
        public List<ItemTag> TagsList;
        
        public TagList(List<ItemTag> tagsList)
        {
            TagsList = tagsList;
        }
        
    }
}