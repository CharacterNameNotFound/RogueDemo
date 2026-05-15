using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Effectors
{
    public class UpdateStatValueEffectorPrototype : EffectorPrototype
    {
        public bool IsCritAvailable = false; 
        
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype StatProviderPrototype;
        public ItemStatType ItemStatType;
        public StatSet.StatSetComponent ApplicationType = StatSet.StatSetComponent.CombatBonus;
        
        public override Effector GetEffector()
        {
            return new UpdateStatValueEffector(TargetSelectorPrototypes.GetTargetSelector(), StatProviderPrototype.GetStatProvider(), ItemStatType, IsCritAvailable, ApplicationType); 
        }
    }
}