using System;
using System.Collections.Generic;
using System.Linq;
using Game.Utilities.Shuffling;
using GameWideSystems.RNGManagement;
using Newtonsoft.Json;

namespace Game.GameMode.StorySession.Utilities.Decks
{
    [Serializable]
    public class Deck<T> : IDeck<T>
    {
        [JsonProperty]
        protected List<T> ActiveItems;
        [JsonProperty]
        protected List<T> DiscardedItems;

        [NonSerialized]
        protected IRNGProvider FallBackShuffler;

        [JsonConstructor]
        public Deck(List<T> items, List<T> discardedItems)
        {
            ActiveItems = items;
            DiscardedItems = discardedItems;
        }
        
        public Deck(List<T> items, IRNGProvider fallBackShuffler)
        {
            ActiveItems = items;
            DiscardedItems = new List<T>(items.Count);
            
            FallBackShuffler = fallBackShuffler;
        }

        public void SetFallbackRNGProvider(IRNGProvider fallBackShuffler)
        {
            FallBackShuffler = fallBackShuffler;
        }

        public bool Draw(bool isReturnedImmediate, out T result)
        {
            if (ActiveItems.Count == 0)
            {
                if (DiscardedItems.Count == 0)
                {
                    result = default;
                    return false;
                }

                ShuffleInAll(FallBackShuffler);
            }
            
            T last = ActiveItems.Last();
            ActiveItems.RemoveAt(ActiveItems.Count - 1);

            if (isReturnedImmediate)
            {
                DiscardedItems.Add(last);
            }

            result = last;
            return true;
        }

        public void Return(T item)
        {
            DiscardedItems.Add(item);
        }

        public void AppendToActive(List<T> items)
        {
            ActiveItems.AddRange(items);
        }

        public void ShuffleInAll(IRNGProvider rngProvider)
        {
            ActiveItems.AddRange(DiscardedItems);
            DiscardedItems.Clear();
            
            Shuffle(rngProvider);
        }
        
        public void Shuffle(IRNGProvider rngProvider)
        {
            ActiveItems.ShuffleListDurstenfeld(rngProvider);
        }
        
    }
}