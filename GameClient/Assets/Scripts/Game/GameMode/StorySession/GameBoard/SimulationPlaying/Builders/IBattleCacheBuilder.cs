using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Builders
{
    public interface IBattleCacheBuilder
    {
        public BattleCache BattleCache(IGameBoardModelHolder gameBoardModelHolder);
    }
}