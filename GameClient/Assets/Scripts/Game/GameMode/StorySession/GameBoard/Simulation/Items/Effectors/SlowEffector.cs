using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
{
    public class SlowEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider SlowDurationProvider;
        
        public SlowEffector(TargetSelector targetSelector, StatProvider slowDurationProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            SlowDurationProvider = slowDurationProvider;
            IsCritAvailable = isCritAvailable;
        }
    }
}