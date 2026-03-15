using Game.GameMode.StorySession.GameBoard.Simulation.Items.StatProviders;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors
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
    }
}