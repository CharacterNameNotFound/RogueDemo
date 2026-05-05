using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Effectors
{
    public class ApplyBurnEffectorPrototype : EffectorPrototype
    {
        public bool IsCritAvailable = true; 

        
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype StatProviderPrototype;
        public StatSet.StatSetComponent ApplicationType = StatSet.StatSetComponent.CombatBonus;
        
        public override Effector GetEffector()
        {
            return new ApplyBurnEffector(TargetSelectorPrototypes.GetTargetSelector(), StatProviderPrototype.GetStatProvider(), IsCritAvailable, ApplicationType); 
        }
    }
}