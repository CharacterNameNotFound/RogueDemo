using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.TargetSelectors;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.TargetSelectors
{
    public class OwnerItemTargetSelectorPrototype : TargetSelectorPrototype
    {
        public StatProviderPrototype TargetCount;
        public bool SelectNonCooldownItems = false;
        
        public override TargetSelector GetTargetSelector()
        {
            return new OwnerItemTargetSelector(TargetCount.GetStatProvider(), SelectNonCooldownItems);
        }
        
    }
}