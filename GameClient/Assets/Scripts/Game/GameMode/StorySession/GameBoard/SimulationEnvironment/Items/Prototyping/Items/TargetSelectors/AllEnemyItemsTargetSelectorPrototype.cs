using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.TargetSelectors
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