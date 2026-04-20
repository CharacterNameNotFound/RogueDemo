using System.Collections.Generic;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;


namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class BaseStoryContext : IStoryContext
    {
        public CharacterData CharacterData;
        public BattleEncounter[] Bosses;
        
        public List<List<string>> StoryEncounters { get; set; }
        public int Cycle { get; set; } = 0;
        public int Step { get; set; } = 0;
        public GameBoardModel GameBoardModel { get; set; } = new GameBoardModel();


    }
}