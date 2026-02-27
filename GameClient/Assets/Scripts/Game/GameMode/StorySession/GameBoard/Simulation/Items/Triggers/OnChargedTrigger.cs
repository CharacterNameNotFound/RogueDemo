using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Triggers
{
    public class OnChargedTrigger : Trigger
    {
        public List<Effector> Effectors;
        
        public OnChargedTrigger(List<Effector> effectors)
        {
            Effectors = effectors;
        }
    }
}