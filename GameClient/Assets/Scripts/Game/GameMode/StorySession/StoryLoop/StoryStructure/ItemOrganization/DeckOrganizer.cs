using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public class DeckOrganizer
    {
        private Dictionary<ItemRarity, ItemDeck> _deckRegistry;

        public DeckOrganizer()
        {
            
        }
        
        public void Initialize(Dictionary<ItemRarity, ItemDeck> itemDecks)
        {
            _deckRegistry = itemDecks;
        }

        public void CleanUp()
        {
            _deckRegistry = null;
        }
        
        
        
    }
}