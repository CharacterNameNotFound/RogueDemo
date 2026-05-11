using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects
{
    public class RegenerationHeroStatusEffect : IHeroStatusEffect
    {
        public ItemStatEntry RegenerationIntensity;
        
        public float MaxCooldown = 1;
        public float Cooldown = 1;
        
    }
}