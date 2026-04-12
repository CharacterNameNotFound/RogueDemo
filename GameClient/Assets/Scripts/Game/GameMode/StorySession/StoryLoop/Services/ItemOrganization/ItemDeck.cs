using System.Collections.Generic;
using Game.GameMode.StorySession.Utilities;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemDeck : Deck<string>
    {
        public ItemDeck(List<string> items) : base(items)
        {
        }
    }
}