using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects
{
    public interface IHeroStatusEffectManager
    {
        public void Update(BattleSideCache battleSideCache, int owner, float deltaTime);
        public UniTask PostBattleReset(BattleCache battleCache, CancellationToken cancellationToken);
    }
}