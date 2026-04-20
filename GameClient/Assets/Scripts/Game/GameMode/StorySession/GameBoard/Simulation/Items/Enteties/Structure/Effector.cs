namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure
{
    public abstract class Effector
    {
        public bool IsCritAvailable;

        public abstract Effector GetCopy();
    }
}