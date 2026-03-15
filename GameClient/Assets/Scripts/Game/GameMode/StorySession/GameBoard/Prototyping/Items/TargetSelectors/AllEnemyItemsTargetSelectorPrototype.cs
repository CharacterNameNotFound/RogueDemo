using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.TargetSelectors;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.TargetSelectors
{
    public class AllEnemyItemsTargetSelectorPrototype : TargetSelectorPrototype
    {
        public bool SelectNonCooldownItems = false;
        
        public override TargetSelector GetTargetSelector()
        {
            return new AllEnemyItemsTargetSelector(SelectNonCooldownItems);
        }
    }
}