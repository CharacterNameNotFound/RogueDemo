using System.Collections.Generic;

namespace Game.GameMode.StorySession.Utilities
{
    public interface IDeckOrganizer<TKey, TDeckContent>
    {
        public void Initialize(Dictionary<TKey, IDeck<TDeckContent>> itemDecks);
        public void CleanUp();
        public void ShuffleAll();
    }
}