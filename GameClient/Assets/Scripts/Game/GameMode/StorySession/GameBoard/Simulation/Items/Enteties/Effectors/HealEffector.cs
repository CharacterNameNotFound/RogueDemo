using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors
{
    public class HealEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider HealStatProvider;

        public HealEffector(TargetSelector targetSelector, StatProvider healStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            HealStatProvider = healStatProvider;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new HealEffector(TargetSelector.GetCopy(), HealStatProvider.GetCopy(), IsCritAvailable);
        }
    }
}