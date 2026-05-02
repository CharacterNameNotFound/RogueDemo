using System.Collections.Generic;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;


namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class BaseStoryContext : IStoryContext
    {
        public CharacterData CharacterData;
        public BattleEncounter[] Bosses;
        
        public List<List<string>> StoryEncounters { get; set; }
        
        public GameBoardModel GameBoardModel { get; set; } = new GameBoardModel();

        
        public int Cycle
        {
            get => GameBoardModel.Cycle; 
            set => GameBoardModel.Cycle = value; 
        }

        public int Step
        {
            get => GameBoardModel.Step;
            set => GameBoardModel.Step = value;
        }

    }
}