using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data
{
    public class BattleSideCache
    {
        public List<ItemCache> ItemCache;
        public HeroStats HeroStats;
        public ItemBoardModel Model;

        public BattleSideCache(List<ItemCache> itemCache, HeroStats heroStats, ItemBoardModel model)
        {
            ItemCache = itemCache;
            HeroStats = heroStats;
            Model = model;
        }
        
    }
}