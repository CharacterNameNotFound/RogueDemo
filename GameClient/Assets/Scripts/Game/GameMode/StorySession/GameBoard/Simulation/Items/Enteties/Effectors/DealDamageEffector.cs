using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors
{
    public class DealDamageEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider DamageStatProvider;

        public DealDamageEffector(TargetSelector targetSelector, StatProvider damageStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            DamageStatProvider = damageStatProvider;
            IsCritAvailable = isCritAvailable;
        }
    }
}