using System.Collections.Generic;
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
    }
}