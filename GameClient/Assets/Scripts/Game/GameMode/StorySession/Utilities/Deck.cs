using System.Collections.Generic;
using Game.Utilities.Shuffling;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.Utilities
{
    public class Deck<T> : IDeck<T>
    {
        protected List<T> ActiveItems;
        protected List<T> DiscardedItems;

        public Deck(List<T> items)
        {
            ActiveItems = items;
            DiscardedItems = new List<T>(items.Count);
        }

        public void AppendToActive(List<T> items)
        {
            ActiveItems.AddRange(items);
        }

        public void Shuffle(IRNGProvider rngProvider)
        {
            ActiveItems.ShuffleListDurstenfeld(rngProvider);
        }
    }
}