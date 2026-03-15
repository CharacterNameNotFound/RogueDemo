using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.TargetSelectors
{
    public class AllEnemyItemsTargetSelector : TargetSelector
    {
        public bool SelectNonCooldownItems;

        public AllEnemyItemsTargetSelector(bool selectNonCooldownItems)
        {
            SelectNonCooldownItems = selectNonCooldownItems;
        }
    }
}