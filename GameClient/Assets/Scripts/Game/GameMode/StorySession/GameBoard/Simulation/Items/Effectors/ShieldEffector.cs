using Game.GameMode.StorySession.GameBoard.Simulation.Items.StatProviders;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
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
    }
}