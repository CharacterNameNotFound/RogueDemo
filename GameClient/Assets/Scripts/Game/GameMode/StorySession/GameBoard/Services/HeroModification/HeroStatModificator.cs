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

        public void DealDamage(float value, HeroGroup heroGroup)
        {
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(heroGroup, _gameBoardModelHolder.GameBoardModel);
            
            UpdateHpInternal(-value, heroStats, heroGroup);
        }

        public void Heal(float value, HeroGroup heroGroup)
        {
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(heroGroup, _gameBoardModelHolder.GameBoardModel);
            
            UpdateHpInternal(value, heroStats, heroGroup);
        }

        public void UpdateHp(float value, HeroGroup heroGroup)
        {
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(heroGroup, _gameBoardModelHolder.GameBoardModel);

            UpdateHpInternal(value, heroStats, heroGroup);
        }

        private void UpdateHpInternal(float value, HeroStats heroStats, HeroGroup heroGroup)
        {
            heroStats.Hp += value;
            heroStats.Hp = Mathf.Min(heroStats.MaxHp, heroStats.Hp);
            
            _heroesHpDrawer.UpdateHeroHpBar(heroGroup);
        }
        
    }
}