using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Triggers
{
    [Serializable]
    public class HealOnCooldownComponent : TriggerComponent
    {
        public HealOnCooldownComponent()
        {
            
        }
        
        public override ItemComponent GetCopy()
        {
            return new HealOnCooldownComponent();
        }
    }
}