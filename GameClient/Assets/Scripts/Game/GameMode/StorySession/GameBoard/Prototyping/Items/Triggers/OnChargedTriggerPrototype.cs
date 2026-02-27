using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Triggers;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Triggers
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