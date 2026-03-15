using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Effectors
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