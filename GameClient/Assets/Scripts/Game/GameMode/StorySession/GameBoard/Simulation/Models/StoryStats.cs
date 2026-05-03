namespace Game.GameMode.StorySession.GameBoard.Simulation.Models
{
    public class StoryStats
    {
        public int Cycle = 0;
        public int Step = 0;

        public StoryStats(int cycle, int step)
        {
            Cycle = cycle;
            Step = step;
        }
    }
}