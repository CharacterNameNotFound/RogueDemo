using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
{
    public class HasteEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider HasteDurationProvider;

        public HasteEffector(
            TargetSelector targetSelector, 
            StatProvider hasteDurationProvider, 
            bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            HasteDurationProvider = hasteDurationProvider;
            IsCritAvailable = isCritAvailable;
        }
    }
}