using System.Collections.Generic;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.Utilities
{
    public class DeckOrganizer<TKey, TDeckContent> : IDeckOrganizer<TKey, TDeckContent>
    {
        private Dictionary<TKey, IDeck<TDeckContent>> _deckRegistry;
        private IRNGProvider _randomProvider;

        public DeckOrganizer(IRNGManager rngManager)
        {
            _randomProvider = rngManager.GetRandomProvider(RNGGroup.CardShuffler);
        }

        public void Initialize(Dictionary<TKey, IDeck<TDeckContent>> itemDecks)
        {
            _deckRegistry = itemDecks;
        }

        public void CleanUp()
        {
            _deckRegistry = null;
        }

        public void ShuffleAll()
        {
            foreach (IDeck<TDeckContent> itemDeck in _deckRegistry.Values)
            {
                itemDeck.Shuffle(_randomProvider);
            }
        }
    }
}