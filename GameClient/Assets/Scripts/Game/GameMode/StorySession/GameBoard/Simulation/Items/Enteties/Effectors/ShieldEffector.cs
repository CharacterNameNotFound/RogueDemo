using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors
{
    public class ShieldEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider ShieldStatProvider;

        public ShieldEffector(TargetSelector targetSelector, StatProvider shieldStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            ShieldStatProvider = shieldStatProvider;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new ShieldEffector(TargetSelector.GetCopy(), ShieldStatProvider.GetCopy(), IsCritAvailable);
        }
    }
}