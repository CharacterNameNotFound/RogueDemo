using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public class DeckOrganizer : IDeckOrganizer
    {
        private Dictionary<ItemRarity, ItemDeck> _deckRegistry;
        private IRNGProvider _randomProvider;

        public DeckOrganizer(IRNGManager rngManager)
        {
            _randomProvider = rngManager.GetRandomProvider(RNGGroup.CardShuffler);
        }
        
        public void Initialize(Dictionary<ItemRarity, ItemDeck> itemDecks)
        {
            _deckRegistry = itemDecks;
        }

        public void CleanUp()
        {
            _deckRegistry = null;
        }

        public void ShuffleAll()
        {
            foreach (ItemDeck itemDeck in _deckRegistry.Values)
            {
                itemDeck.Shuffle(_randomProvider);
            }
        }
        
        
    }
}