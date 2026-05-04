using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.HeroModification
{
    public interface IHeroStatModificator
    {
        public void UpdateHp(float value, HeroGroup heroGroup);
    }
}