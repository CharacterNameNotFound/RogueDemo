using System.Collections.Generic;
using Game.Utilities.Shuffling;
using GameWideSystems.RNGManagement;

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

        public void Shuffle(IRNGProvider rngProvider)
        {
            ActiveItems.ShuffleListDurstenfeld(rngProvider);
        }
        
    }
}