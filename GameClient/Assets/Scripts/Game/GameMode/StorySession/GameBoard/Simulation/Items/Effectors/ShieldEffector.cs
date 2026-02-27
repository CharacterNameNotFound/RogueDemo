using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
{
    public class ShieldEffector : Effector
    {
        public TargetSelector TargetSelector;

        public ShieldEffector(TargetSelector targetSelector)
        {
            TargetSelector = targetSelector;
        }
    }
}