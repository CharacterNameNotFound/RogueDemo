using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors
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
        
        public override Effector GetCopy()
        {
            return new SlowEffector(TargetSelector.GetCopy(), SlowDurationProvider.GetCopy(), IsCritAvailable);
        }
    }
}