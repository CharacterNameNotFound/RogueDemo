using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities
{
    public static class GameBoardModelShortcuts
    {
        public static HeroStats HeroGroupToHeroStats(HeroGroup heroGroup, GameBoardModel gameBoardModel)
        {
            return heroGroup switch {
                HeroGroup.Player => gameBoardModel.PlayerHeroStats,
                HeroGroup.Encounter => gameBoardModel.EncounterHeroStats,
                _ => throw new ArgumentOutOfRangeException(nameof(heroGroup), heroGroup, null)
            };
        }
    }
}