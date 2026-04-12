using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Effectors
{
    public class HasteEffectorPrototype : EffectorPrototype
    {
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype HasteDurationProvider;
        
        public override Effector GetEffector()
        {
            return new HasteEffector(TargetSelectorPrototypes.GetTargetSelector(), HasteDurationProvider.GetStatProvider(), false); 
        }
    }
}