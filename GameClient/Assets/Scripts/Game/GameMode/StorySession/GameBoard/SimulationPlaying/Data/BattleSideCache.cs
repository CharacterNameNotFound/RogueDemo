using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data
{
    public class BattleSideCache
    {
        public List<ItemCache> ItemCache;
        public HeroStats HeroStats;
        public ItemBoardModel Model;
        public Dictionary<Type, IHeroStatusEffect> HeroStatusEffects;
        
        
        public BattleSideCache(
            List<ItemCache> itemCache, 
            HeroStats heroStats, 
            ItemBoardModel model, 
            Dictionary<Type, IHeroStatusEffect> heroStatusEffects)
        {
            ItemCache = itemCache;
            HeroStats = heroStats;
            Model = model;
            HeroStatusEffects = heroStatusEffects;
        }
        
    }
}