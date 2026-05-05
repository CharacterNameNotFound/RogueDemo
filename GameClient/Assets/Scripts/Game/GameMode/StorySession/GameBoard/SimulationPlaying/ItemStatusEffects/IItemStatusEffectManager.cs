using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public interface IItemStatusEffectManager
    {
        public void Update(List<ItemCache> items, float deltaTime);
    }
}