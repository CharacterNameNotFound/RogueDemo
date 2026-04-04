using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public interface IDeckOrganizer
    {
        public void Initialize(Dictionary<ItemRarity, ItemDeck> itemDecks);
        public void CleanUp();
        public void ShuffleAll();
    }
}