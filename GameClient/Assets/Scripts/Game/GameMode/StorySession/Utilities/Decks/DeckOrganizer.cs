using System.Collections.Generic;
using GameWideSystems.RNGManagement;
using Newtonsoft.Json;

namespace Game.GameMode.StorySession.Utilities.Decks
{
    public class DeckOrganizer<TKey, TDeckContent> : IDeckOrganizer<TKey, TDeckContent>
    {
        protected Dictionary<TKey, IDeck<TDeckContent>> DeckRegistry;
        protected IRNGProvider RandomProvider;

        public DeckOrganizer(IRNGManager rngManager)
        {
            RandomProvider = rngManager.GetRandomProvider(RNGGroup.CardShuffler);
        }

        public void Initialize(Dictionary<TKey, IDeck<TDeckContent>> itemDecks)
        {
            DeckRegistry = itemDecks;
        }

        public void CleanUp()
        {
            DeckRegistry = null;
        }

        public bool Draw(TKey deckKey, bool isReturnedImmediate, out TDeckContent result)
        {
            IDeck<TDeckContent> deck = GetDeck(deckKey);

            return deck.Draw(isReturnedImmediate, out result);
        }

        public void Return(TKey deckKey, TDeckContent item)
        {
            IDeck<TDeckContent> deck = GetDeck(deckKey);
            
            deck.Return(item);
        }

        public void ShuffleInAllAll()
        {
            foreach (IDeck<TDeckContent> item in DeckRegistry.Values)
            {
                item.ShuffleInAll(RandomProvider);
            }
        }

        public void ShuffleAll()
        {
            foreach (IDeck<TDeckContent> itemDeck in DeckRegistry.Values)
            {
                itemDeck.Shuffle(RandomProvider);
            }
        }

        public DeckOrganizerState GetState(JsonSerializerSettings settings)
        {
            string deckRegistry = JsonConvert.SerializeObject(DeckRegistry, settings);

            return new DeckOrganizerState(deckRegistry);
        }

        public void RestoreState(DeckOrganizerState state, JsonSerializerSettings settings, IRNGProvider rngProvider)
        {
            DeckRegistry = JsonConvert.DeserializeObject<Dictionary<TKey, IDeck<TDeckContent>>>(state.DeckRegistry, settings);
            
            foreach (IDeck<TDeckContent> item in DeckRegistry.Values)
            {
                item.SetFallbackRNGProvider(rngProvider);
            }
            
        }

        private IDeck<TDeckContent> GetDeck(TKey deckKey)
        {
            if (!DeckRegistry.TryGetValue(deckKey, out IDeck<TDeckContent> deck))
            {
                throw new KeyNotFoundException($"{deckKey.ToString()} not found in Encounter Deck list");
            }

            return deck;
        }
        
    }
}