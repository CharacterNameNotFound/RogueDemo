using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Effectors
{
    public class ShieldEffectorPrototype : EffectorPrototype
    {
        public bool IsCritAvailable = true;
        
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype StatProviderPrototype;
        
        public override Effector GetEffector()
        {
            return new ShieldEffector(TargetSelectorPrototypes.GetTargetSelector(), StatProviderPrototype.GetStatProvider(), IsCritAvailable);
        }
        
    }
}