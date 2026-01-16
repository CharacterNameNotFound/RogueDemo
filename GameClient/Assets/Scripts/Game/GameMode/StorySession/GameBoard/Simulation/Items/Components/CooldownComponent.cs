using System;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components
{
    [Serializable]
    public class CooldownComponent : ItemComponent
    {
        public StatSet ItemCooldown;

        public CooldownComponent()
        {
            
        }
        
        public CooldownComponent(StatSet itemCooldown)
        {
            ItemCooldown = itemCooldown;
        }
        
        public override ItemComponent GetCopy()
        {
            return new CooldownComponent(ItemCooldown.GetCopy());
        }
    }
}