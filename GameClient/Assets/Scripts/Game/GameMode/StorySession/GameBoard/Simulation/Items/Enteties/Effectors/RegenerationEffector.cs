using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
{
    public class RegenerationEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider RegenerationStatProvider;

        public RegenerationEffector(TargetSelector targetSelector, StatProvider regenerationStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            RegenerationStatProvider = regenerationStatProvider;
            IsCritAvailable = isCritAvailable;
        }
    }
}