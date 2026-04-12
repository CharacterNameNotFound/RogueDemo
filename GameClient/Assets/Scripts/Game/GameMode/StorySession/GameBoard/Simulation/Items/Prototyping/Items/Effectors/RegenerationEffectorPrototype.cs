using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Effectors
{
    public class RegenerationEffectorPrototype : EffectorPrototype
    {
        public bool IsCritAvailable = true;
        
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype StatProviderPrototype;
        
        public override Effector GetEffector()
        {
            return new RegenerationEffector(TargetSelectorPrototypes.GetTargetSelector(), StatProviderPrototype.GetStatProvider(), IsCritAvailable); 
        }
    }
}