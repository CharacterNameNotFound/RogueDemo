using System;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.GameBoard.Simulation.Models;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Elements;

namespace Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing
{
    public class HeroesHpDrawer : IHeroesHpDrawer
    {
        private GameBoardHolder _gameBoardHolder;
        private IGameBoardModelHolder _gameBoardModelHolder;

        public HeroesHpDrawer(GameBoardHolder gameBoardHolder, IGameBoardModelHolder gameBoardModelHolder)
        {
            _gameBoardHolder = gameBoardHolder;
            _gameBoardModelHolder = gameBoardModelHolder;
        }

        public void UpdateHeroesHpBars()
        {
            UpdateHeroHpBar(HeroGroup.Player);
            UpdateHeroHpBar(HeroGroup.Encounter);
        }

        public void UpdateHeroHpBar(HeroGroup hero)
        {
            HpBarComponent boardHpBar = hero switch
            {
                HeroGroup.Player => _gameBoardHolder.GameBoardComponent.PlayerBoard.HpBar,
                HeroGroup.Encounter => _gameBoardHolder.GameBoardComponent.EncounterBoard.BattleView.HpBar,
                _ => throw new ArgumentOutOfRangeException(nameof(hero), hero, null)
            };
            HeroStats heroStats = hero switch {
                HeroGroup.Player => _gameBoardModelHolder.GameBoardModel.PlayerHeroStats,
                HeroGroup.Encounter => _gameBoardModelHolder.GameBoardModel.EncounterHeroStats,
                _ => throw new ArgumentOutOfRangeException(nameof(hero), hero, null)
            };
            
            UpdateHpBar(boardHpBar, heroStats);
        }

        private void UpdateHpBar(HpBarComponent boardHpBar, HeroStats heroStats)
        {
            boardHpBar.UpdateHpBar(heroStats.Hp, heroStats.MaxHp);
        }
        
    }
}