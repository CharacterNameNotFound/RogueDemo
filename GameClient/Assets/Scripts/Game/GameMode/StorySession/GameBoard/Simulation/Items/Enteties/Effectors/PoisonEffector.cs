using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors
{
    public class PoisonEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider PoisonStatProvider;

        public PoisonEffector(TargetSelector targetSelector, StatProvider poisonStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            PoisonStatProvider = poisonStatProvider;
            IsCritAvailable = isCritAvailable;
        }
    }
}