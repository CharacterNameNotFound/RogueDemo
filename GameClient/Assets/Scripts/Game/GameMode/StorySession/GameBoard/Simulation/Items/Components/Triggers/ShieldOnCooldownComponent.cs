using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Triggers
{
    [Serializable]
    public class ShieldOnCooldownComponent : TriggerComponent
    {
        public ShieldOnCooldownComponent()
        {
            
        }
        
        public override ItemComponent GetCopy()
        {
            return new ShieldOnCooldownComponent();
        }
    }
}