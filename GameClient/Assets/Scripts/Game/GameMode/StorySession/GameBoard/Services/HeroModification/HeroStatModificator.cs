using System;
using Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes;
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

        public void AddShield(float value, HeroGroup heroGroup)
        {
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(heroGroup, _gameBoardModelHolder.GameBoardModel);
            
            value = Mathf.RoundToInt(value);

            heroStats.Shield += value;
            
            _heroesHpDrawer.UpdateHeroHpBar(heroGroup);
        }

        public void DealDamage(float value, HeroGroup heroGroup)
        {
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(heroGroup, _gameBoardModelHolder.GameBoardModel);

            value = Mathf.RoundToInt(value);
            
            float damage = Mathf.Min(value, heroStats.Shield);
            value -= damage;
            heroStats.Shield -= damage;

            if (value == 0)
            {
                _heroesHpDrawer.UpdateHeroHpBar(heroGroup);
                return;
            }
            
            UpdateHpInternal(-value, heroStats, heroGroup);
        }

        public void Heal(float value, HeroGroup heroGroup)
        {
            value = Mathf.RoundToInt(value);
            
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(heroGroup, _gameBoardModelHolder.GameBoardModel);
            
            UpdateHpInternal(value, heroStats, heroGroup);
        }

        public void UpdateHp(float value, HeroGroup heroGroup)
        {
            value = Mathf.RoundToInt(value);
            
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(heroGroup, _gameBoardModelHolder.GameBoardModel);

            UpdateHpInternal(value, heroStats, heroGroup);
        }

        public void PostBattleReset()
        {
            HeroStats heroStats = GameBoardShortcuts.HeroGroupToHeroStats(HeroGroup.Player, _gameBoardModelHolder.GameBoardModel);

            heroStats.Shield = 0;
            heroStats.Hp = heroStats.MaxHp;
            
            _heroesHpDrawer.UpdateHeroHpBar(HeroGroup.Player);
        }

        private void UpdateHpInternal(float value, HeroStats heroStats, HeroGroup heroGroup)
        {
            value = Mathf.RoundToInt(value);

            heroStats.Hp += value;
            heroStats.Hp = Mathf.Min(heroStats.MaxHp, heroStats.Hp);
            
            _heroesHpDrawer.UpdateHeroHpBar(heroGroup);
        }
        
    }
}