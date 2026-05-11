using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils
{
    public static class CacheShortcuts
    {
        public static Item GetItem(int index, int owner, BattleCache battleCache)
        {
            return battleCache.Get(owner).Model.Items[index];
        }
        
        
    }
}