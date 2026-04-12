using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.TargetSelectors
{
    public class AllOwnerItemsSelectorPrototype : TargetSelectorPrototype
    {
        public bool SelectNonCooldownItems = false;
        
        public override TargetSelector GetTargetSelector()
        {
            return new AllOwnerItemsSelector(SelectNonCooldownItems);
        }
    }
}