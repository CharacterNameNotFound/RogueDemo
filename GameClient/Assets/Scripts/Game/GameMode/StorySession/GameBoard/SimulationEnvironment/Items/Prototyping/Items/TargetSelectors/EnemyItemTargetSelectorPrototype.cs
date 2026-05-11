using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.TargetSelectors
{
    public class EnemyItemTargetSelectorPrototype : TargetSelectorPrototype
    {
        public StatProviderPrototype TargetCount;
        public bool SelectNonCooldownItems = false;
        
        public override TargetSelector GetTargetSelector()
        {
            return new EnemyItemTargetSelector(TargetCount.GetStatProvider(), SelectNonCooldownItems);
        }
    }
}