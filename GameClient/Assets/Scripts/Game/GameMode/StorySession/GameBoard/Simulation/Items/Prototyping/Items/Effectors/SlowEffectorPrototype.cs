using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Effectors
{
    public class SlowEffectorPrototype : EffectorPrototype
    {
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype SlowDurationProvider;
        
        public override Effector GetEffector()
        {
            return new SlowEffector(TargetSelectorPrototypes.GetTargetSelector(), SlowDurationProvider.GetStatProvider(), false); 
        }
    }
}