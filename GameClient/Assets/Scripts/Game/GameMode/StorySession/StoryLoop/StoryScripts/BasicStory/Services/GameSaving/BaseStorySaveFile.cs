using System;
using System.Collections.Generic;
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
        public int Cycle;
        public int Step;
        
        // encounters
        public DeckOrganizerState EncounterDeckOrganizerState;
        public List<string> EncounterRegistryIds;
        
        
        // items
        public DeckOrganizerState ItemDeckOrganizerState;
        public List<string> ItemRegistryIds;

    }
}