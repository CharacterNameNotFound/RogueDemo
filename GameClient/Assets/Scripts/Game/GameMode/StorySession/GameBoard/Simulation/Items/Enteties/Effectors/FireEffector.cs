using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors
{
    public class FireEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider FireStatProvider;

        public FireEffector(TargetSelector targetSelector, StatProvider fireStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            FireStatProvider = fireStatProvider;
            IsCritAvailable = isCritAvailable;
        }
        
    }
}