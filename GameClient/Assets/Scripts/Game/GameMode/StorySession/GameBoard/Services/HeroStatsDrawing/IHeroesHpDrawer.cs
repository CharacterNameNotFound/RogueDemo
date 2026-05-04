using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing
{
    public interface IHeroesHpDrawer
    {
        public void UpdateHeroesHpBars();
        public void UpdateHeroHpBar(HeroGroup hero);
    }
}