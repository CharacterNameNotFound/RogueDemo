using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes
{
    [Serializable]
    public class HeroStats
    {
        public float MaxHp;
        public float Hp;
        public float Shield;

        public HeroStats()
        {
            
        }
        
        public HeroStats(float maxHp, float hp, float shield)
        {
            MaxHp = maxHp;
            Hp = hp;
            Shield = shield;
        }
        
        
    }
}