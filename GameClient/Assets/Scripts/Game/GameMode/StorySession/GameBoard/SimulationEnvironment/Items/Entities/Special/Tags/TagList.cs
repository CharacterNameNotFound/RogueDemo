using System;
using System.Collections.Generic;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags
{
    [Serializable]
    public class TagList
    {
        public List<ItemTag> TagsList;
        
        public TagList(List<ItemTag> tagsList)
        {
            TagsList = tagsList;
        }

        public bool ContainsTag(ItemTag itemTag)
        {
            return TagsList.Contains(itemTag);
        }

        public TagList GetCopy()
        {
            return new TagList(TagsList);
        }
    }
}