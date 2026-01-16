using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Triggers
{
    [Serializable]
    public class DamageOnCooldownComponent : TriggerComponent
    {

        public DamageOnCooldownComponent()
        {
            
        }
        
        public override ItemComponent GetCopy()
        {
            return new DamageOnCooldownComponent();
        }
    }
}