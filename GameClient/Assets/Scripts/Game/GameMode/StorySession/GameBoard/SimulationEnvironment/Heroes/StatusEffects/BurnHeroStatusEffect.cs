using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects
{
    public class BurnHeroStatusEffect : IHeroStatusEffect
    {
        public StatSet BurnIntensity;

        public float MaxCooldown = 1;
        public float Cooldown = 1;
        
    }
}