using System.Collections.Generic;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public class ItemDeck
    {
        public List<string> ActiveItems;
        public List<string> DiscardedItems;

        public ItemDeck(List<string> items)
        {
            ActiveItems = items;
            DiscardedItems = new List<string>(items.Count);
        }

        public void AppendToActive(List<string> items)
        {
            ActiveItems.AddRange(items);
        }
        
    }
}