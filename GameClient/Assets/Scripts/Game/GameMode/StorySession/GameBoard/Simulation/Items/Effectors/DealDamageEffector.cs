using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
{
    public class DealDamageEffector : Effector
    {
        public TargetSelector TargetSelector;

        public DealDamageEffector(TargetSelector targetSelector)
        {
            TargetSelector = targetSelector;
        }
    }
}