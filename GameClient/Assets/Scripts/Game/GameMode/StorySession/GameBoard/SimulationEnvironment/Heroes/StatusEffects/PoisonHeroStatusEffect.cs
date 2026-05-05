using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects
{
    public class PoisonHeroStatusEffect : IHeroStatusEffect
    {
        public StatSet PoisonIntensity;
        
        public float MaxCooldown = 1;
        public float Cooldown = 1;
        
    }
}