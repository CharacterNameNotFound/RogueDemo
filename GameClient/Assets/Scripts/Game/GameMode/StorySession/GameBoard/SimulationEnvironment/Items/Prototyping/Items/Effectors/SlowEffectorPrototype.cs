using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Effectors
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