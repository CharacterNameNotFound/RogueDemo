using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Effectors
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