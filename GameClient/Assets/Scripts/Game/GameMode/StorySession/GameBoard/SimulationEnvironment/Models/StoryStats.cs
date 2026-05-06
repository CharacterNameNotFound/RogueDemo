using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models
{
    [Serializable]
    public class StoryStats
    {
        public int Cycle = 0;
        public int Step = 0;

        public StoryStats()
        {
            
        }   
        
        public StoryStats(int cycle, int step)
        {
            Cycle = cycle;
            Step = step;
        }
    }
}