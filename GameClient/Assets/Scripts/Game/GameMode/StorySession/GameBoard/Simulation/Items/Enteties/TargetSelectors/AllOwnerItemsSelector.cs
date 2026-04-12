using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.TargetSelectors
{
    public class AllOwnerItemsSelector : TargetSelector
    {
        public bool SelectNonCooldownItems;

        public AllOwnerItemsSelector(bool selectNonCooldownItems)
        {
            SelectNonCooldownItems = selectNonCooldownItems;
        }
    }
}