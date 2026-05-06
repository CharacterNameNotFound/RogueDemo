using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;
using Game.GameMode.StorySession.StoryLoop.Services.ItemLineSaveLoad;
using Game.GameMode.StorySession.Utilities.Decks;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services.GameSaving
{
    [Serializable]
    public class BaseStorySaveFile
    {
        // Base Story Context
        public string CharacterId;
        public string[] Bosses;
        public List<List<string>> StoryEncounters;
        
        // encounters
        public DeckOrganizerState EncounterDeckOrganizerState;
        public List<string> EncounterRegistryIds;
        
        
        // items
        public DeckOrganizerState ItemDeckOrganizerState;
        public List<string> ItemRegistryIds;
        
        // Player
        public StoryStats StoryStats;
        public PlayerStats PlayerStats;
        public HeroStats PlayerHeroStats;
        
        public ItemLineSaveData PlayerItemsData;
        
        
    }
}