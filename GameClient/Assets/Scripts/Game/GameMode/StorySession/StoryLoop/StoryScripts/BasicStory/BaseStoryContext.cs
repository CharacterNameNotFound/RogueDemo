using System.Collections.Generic;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles;


namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class BaseStoryContext : IStoryContext
    {
        public CharacterData CharacterData;
        public BattleEncounter[] Bosses;
        
        public List<List<string>> StoryEncounters { get; set; }

    }
}