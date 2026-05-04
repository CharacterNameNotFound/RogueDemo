using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Triggers
{
    public class OnChargedTriggerPrototype : TriggerPrototype
    {
        public List<EffectorPrototype> Effectors;
        
        public override Trigger GetTrigger()
        {
            return new OnChargedTrigger(Effectors.Select(x => x.GetEffector()).ToList());
        }
    }
}