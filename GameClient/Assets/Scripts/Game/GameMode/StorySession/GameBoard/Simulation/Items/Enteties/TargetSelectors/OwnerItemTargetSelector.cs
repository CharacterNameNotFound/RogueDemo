using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.TargetSelectors
{
    public class OwnerItemTargetSelector : TargetSelector
    {
        public StatProvider TargetCount;
        public bool SelectNonCooldownItems;

        public OwnerItemTargetSelector(StatProvider targetCount, bool selectNonCooldownItems)
        {
            TargetCount = targetCount;
            SelectNonCooldownItems = selectNonCooldownItems;
        }
    }
}