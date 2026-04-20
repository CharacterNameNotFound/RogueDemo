using System.Collections.Generic;
using GameWideSystems.RNGManagement;
using Newtonsoft.Json;

namespace Game.GameMode.StorySession.Utilities.Decks
{
    public interface IDeckOrganizer<TKey, TDeckContent>
    {
        public void Initialize(Dictionary<TKey, IDeck<TDeckContent>> itemDecks);
        public void CleanUp();
        public bool Draw(TKey deckKey, bool isReturnedImmediate, out TDeckContent result);
        public void Return(TKey deckKey, TDeckContent item);
        public void ShuffleInAllAll();
        public void ShuffleAll();
        public DeckOrganizerState GetState(JsonSerializerSettings settings);
        public void RestoreState(DeckOrganizerState state, JsonSerializerSettings settings, IRNGProvider rngProvider);
    }
}