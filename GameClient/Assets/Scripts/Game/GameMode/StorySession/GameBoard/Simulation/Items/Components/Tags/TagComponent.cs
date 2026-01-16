using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Tags
{
    [Serializable]
    public class TagComponent : ItemComponent
    {
        public List<ItemTag> TagsList;

        public TagComponent()
        {
            
        }
        
        private TagComponent(List<ItemTag> tagsList)
        {
            TagsList = tagsList;
        }

        public override ItemComponent GetCopy()
        {
            return new TagComponent(TagsList.ToList());
        }
    }
}