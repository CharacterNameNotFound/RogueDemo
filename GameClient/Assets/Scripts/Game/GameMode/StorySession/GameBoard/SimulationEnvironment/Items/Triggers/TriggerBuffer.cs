using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers
{
    public class TriggerBuffer
    {
        private const int TriggerListSize = 20;
        
        public List<Trigger> NewTriggers;
        public List<Trigger> LastFrameTriggers;

        public TriggerBuffer()
        {
            NewTriggers = new(TriggerListSize);
            LastFrameTriggers = new (TriggerListSize);
        }

        public void AddTrigger(Trigger trigger)
        {
            NewTriggers.Add(trigger);
        }
        
        /// <summary>
        /// Moves triggers from NewTriggers to LastFrameTriggers in preparation for new pass
        /// </summary>
        public void TransitTriggers()
        {
            LastFrameTriggers.Clear();

            (NewTriggers, LastFrameTriggers) = (LastFrameTriggers, NewTriggers);
        }

        
    }
}