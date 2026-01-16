using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Triggers
{
    [Serializable]
    public class TriggerComponent : ItemComponent
    {
        public TriggerComponent()
        {
            
        }

        // newtonsoft json serializer requires this class not to be abstract for proper deserialization
        public override ItemComponent GetCopy()
        {
            return null;
        }
    }
}