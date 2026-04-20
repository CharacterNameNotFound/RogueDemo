using System.Collections.Generic;
using Game.GameMode.StorySession.Utilities;
using Game.GameMode.StorySession.Utilities.Decks;
using GameWideSystems.RNGManagement;
using Newtonsoft.Json;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemDeck : Deck<string>
    {
        [JsonConstructor]
        public ItemDeck(List<string> items, List<string> discardedItems) : base(items, discardedItems)
        {
        }
        
        public ItemDeck(List<string> items, IRNGProvider fallBackShuffler) : base(items, fallBackShuffler)
        {
        }
    }
}