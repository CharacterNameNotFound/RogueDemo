using System;
using System.Collections.Generic;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.Tags
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