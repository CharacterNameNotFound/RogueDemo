using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.TargetSelectors
{
    public class AllEnemyItemsTargetSelector : TargetSelector
    {
        public bool SelectNonCooldownItems;

        public AllEnemyItemsTargetSelector(bool selectNonCooldownItems)
        {
            SelectNonCooldownItems = selectNonCooldownItems;
        }

        public override TargetSelector GetCopy()
        {
            return new AllEnemyItemsTargetSelector(SelectNonCooldownItems);
        }
    }
}