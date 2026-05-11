using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.TargetSelectors
{
    public class OwnerTargetSelectorPrototype : TargetSelectorPrototype
    {
        public override TargetSelector GetTargetSelector()
        {
            return new OwnerTargetSelector();
        }
    }
}