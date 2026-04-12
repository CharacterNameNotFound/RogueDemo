using System.Collections.Generic;
using Game.GameMode.StorySession.Utilities;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public class EncounterDeck : Deck<string>
    {
        public EncounterDeck(List<string> items) : base(items)
        {
        }
    }
}