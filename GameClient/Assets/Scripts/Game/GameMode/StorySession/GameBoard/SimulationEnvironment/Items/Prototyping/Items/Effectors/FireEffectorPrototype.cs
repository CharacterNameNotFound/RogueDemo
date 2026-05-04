using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Effectors
{
    public class FireEffectorPrototype : EffectorPrototype
    {
        public bool IsCritAvailable = true; 

        
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype StatProviderPrototype;
        
        public override Effector GetEffector()
        {
            return new FireEffector(TargetSelectorPrototypes.GetTargetSelector(), StatProviderPrototype.GetStatProvider(), IsCritAvailable); 
        }
    }
}