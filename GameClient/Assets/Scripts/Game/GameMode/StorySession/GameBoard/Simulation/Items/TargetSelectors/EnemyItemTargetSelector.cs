using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.TargetSelectors
{
    public class EnemyItemTargetSelector : TargetSelector
    {
        public StatProvider TargetCount;
        public bool SelectNonCooldownItems;

        public EnemyItemTargetSelector(StatProvider targetCount, bool selectNonCooldownItems)
        {
            TargetCount = targetCount;
            SelectNonCooldownItems = selectNonCooldownItems;
        }
    }
}