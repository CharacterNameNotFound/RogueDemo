using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Effectors
{
    public class ApplyHasteEffectorPrototype : EffectorPrototype
    {
        public TargetSelectorPrototype TargetSelectorPrototypes;
        public StatProviderPrototype HasteDurationProvider;
        
        public override Effector GetEffector()
        {
            return new ApplyHasteEffector(TargetSelectorPrototypes.GetTargetSelector(), HasteDurationProvider.GetStatProvider(), false); 
        }
    }
}