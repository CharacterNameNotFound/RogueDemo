using System;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components
{
    [Serializable]
    public class StatsComponent : ItemComponent
    {
        public StatSet Attack;
        public StatSet Defence;
        public StatSet Healing;

        public StatsComponent()
        {
            
        }
        
        public StatsComponent(StatSet attack, StatSet defence, StatSet healing)
        {
            Attack = attack;
            Defence = defence;
            Healing = healing;
        }

        public override ItemComponent GetCopy()
        {
            return new StatsComponent(Attack.GetCopy(), Defence.GetCopy(), Healing.GetCopy());
        }
    }
}