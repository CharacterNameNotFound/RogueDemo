using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
{
    public class HealEffector : Effector
    {
        public TargetSelector TargetSelector;

        public HealEffector(TargetSelector targetSelector)
        {
            TargetSelector = targetSelector;
        }
    }
}