using System.Collections.Generic;
using Game.GameMode.StorySession.Utilities;
using Game.GameMode.StorySession.Utilities.Decks;
using GameWideSystems.RNGManagement;
using Newtonsoft.Json;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public class EncounterDeck : Deck<string>
    {
        [JsonConstructor]
        public EncounterDeck(List<string> items, List<string> discardedItems) : base(items, discardedItems)
        {
        }
        
        public EncounterDeck(List<string> items, IRNGProvider fallBackShuffler) : base(items, fallBackShuffler)
        {
        }
    }
}