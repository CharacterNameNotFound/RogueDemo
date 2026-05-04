using System;
using Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.HeroModification
{
    public class HeroStatModificator : IHeroStatModificator
    {
        private IHeroesHpDrawer _heroesHpDrawer;
        private IGameBoardModelHolder _gameBoardModelHolder;

        public HeroStatModificator(IHeroesHpDrawer heroesHpDrawer, IGameBoardModelHolder gameBoardModelHolder)
        {
            _heroesHpDrawer = heroesHpDrawer;
            _gameBoardModelHolder = gameBoardModelHolder;
        }

        public void UpdateHp(float value, HeroGroup heroGroup)
        {
            HeroStats heroStats = heroGroup switch {
                HeroGroup.Player => _gameBoardModelHolder.GameBoardModel.PlayerHeroStats,
                HeroGroup.Encounter => _gameBoardModelHolder.GameBoardModel.EncounterHeroStats,
                _ => throw new ArgumentOutOfRangeException(nameof(heroGroup), heroGroup, null)
            };

            heroStats.Hp += value;
            heroStats.Hp = Mathf.Min(heroStats.MaxHp, heroStats.Hp);
            
            _heroesHpDrawer.UpdateHeroHpBar(heroGroup);
        }
        
        
    }
}