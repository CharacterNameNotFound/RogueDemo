namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models
{
    public class HeroStats
    {
        public float MaxHp;
        public float Hp;
        public float Shield;

        public HeroStats(float maxHp, float hp, float shield)
        {
            MaxHp = maxHp;
            Hp = hp;
            Shield = shield;
        }
        
        
    }
}