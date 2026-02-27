using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.TargetSelectors;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.TargetSelectors
{
    public class EnemyTargetSelectorPrototype : TargetSelectorPrototype
    {
        public override TargetSelector GetTargetSelector()
        {
            return new EnemySelector();
        }
    }
}