using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Triggers
{
    public class OnChargedTrigger : Trigger
    {
        public List<Effector> Effectors;
        
        public OnChargedTrigger(List<Effector> effectors)
        {
            Effectors = effectors;
        }

        public override Trigger GetCopy()
        {
            return new OnChargedTrigger(Effectors.Select(item => item.GetCopy()).ToList());
        }
    }
}