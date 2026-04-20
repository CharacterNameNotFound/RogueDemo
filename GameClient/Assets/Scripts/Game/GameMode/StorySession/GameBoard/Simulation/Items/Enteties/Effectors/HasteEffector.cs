using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors
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

        public override Effector GetCopy()
        {
            return new HasteEffector(TargetSelector.GetCopy(), HasteDurationProvider.GetCopy(), IsCritAvailable);
        }
    }
}