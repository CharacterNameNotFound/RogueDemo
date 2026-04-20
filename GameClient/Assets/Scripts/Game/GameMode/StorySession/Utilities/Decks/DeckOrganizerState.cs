using System;

namespace Game.GameMode.StorySession.Utilities.Decks
{
    [Serializable]
    public class DeckOrganizerState
    {
        // dictionary for decks held by registry
        public string DeckRegistry;

        public DeckOrganizerState(string deckRegistry)
        {
            DeckRegistry = deckRegistry;
        }
    }
}