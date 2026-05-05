using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.HeroModification
{
    public interface IHeroStatModificator
    {
        public void DealDamage(float value, HeroGroup heroGroup);
        public void Heal(float value, HeroGroup heroGroup);
        public void UpdateHp(float value, HeroGroup heroGroup);
    }
}