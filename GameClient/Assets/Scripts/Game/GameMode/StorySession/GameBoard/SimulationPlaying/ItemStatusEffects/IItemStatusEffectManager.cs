using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public interface IItemStatusEffectManager
    {
        public void Update(List<ItemCache> items, float deltaTime);
        public UniTask PostBattleReset(BattleCache battleCache, CancellationToken cancellationToken);
    }
}