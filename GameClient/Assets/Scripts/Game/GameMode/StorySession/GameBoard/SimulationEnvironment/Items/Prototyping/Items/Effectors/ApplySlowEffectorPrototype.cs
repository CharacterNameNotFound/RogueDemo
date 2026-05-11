using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Effectors
{
    public class ApplySlowEffectorPrototype : EffectorPrototype
    {
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype SlowDurationProvider;
        
        public override Effector GetEffector()
        {
            return new ApplySlowEffector(TargetSelectorPrototypes.GetTargetSelector(), SlowDurationProvider.GetStatProvider(), false); 
        }
    }
}