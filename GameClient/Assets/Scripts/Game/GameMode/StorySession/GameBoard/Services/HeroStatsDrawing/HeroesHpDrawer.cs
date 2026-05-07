using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
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
            
            HeroStats heroStats = GameBoardModelShortcuts.HeroGroupToHeroStats(hero, _gameBoardModelHolder.GameBoardModel);
            
            
            
            UpdateHpBar(boardHpBar, heroStats);
        }

        private void UpdateHpBar(HpBarComponent boardHpBar, HeroStats heroStats)
        {
            boardHpBar.UpdateHpBar(heroStats.Hp, heroStats.MaxHp, heroStats.Shield);
        }
        
    }
}