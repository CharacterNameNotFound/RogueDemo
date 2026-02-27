using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Effectors
{
    public class DealDamageEffectorPrototype : EffectorPrototype
    {
        public TargetSelectorPrototype TargetSelectorPrototypes;
        
        public override Effector GetEffector()
        {
            return new DealDamageEffector(TargetSelectorPrototypes.GetTargetSelector());
        }
        
    }
}