using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public interface IStoryContext
    {
        // cycle, encounter set;
        // cycle <- day * day_length + hour
        public List<List<string>> StoryEncounters { get; set; }
        public int Cycle { get; set; }
        public int Step { get; set; }
        public GameBoardModel GameBoardModel { get; set; }
    }
}