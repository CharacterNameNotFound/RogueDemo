using Game.GameMode.StorySession.GameBoard.Simulation.Items.StatProviders;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
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