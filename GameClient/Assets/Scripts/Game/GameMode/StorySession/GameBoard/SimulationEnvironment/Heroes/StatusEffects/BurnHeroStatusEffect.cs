using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects
{
    public class BurnHeroStatusEffect : IHeroStatusEffect
    {
        public ItemStatEntry BurnIntensity;

        public float MaxCooldown = 1;
        public float Cooldown = 1;
        
    }
}