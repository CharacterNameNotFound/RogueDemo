using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects
{
    public class RegenerationHeroStatusEffect : IHeroStatusEffect
    {
        public StatSet RegenerationIntensity;
        
        public float MaxCooldown = 1;
        public float Cooldown = 1;
        
    }
}