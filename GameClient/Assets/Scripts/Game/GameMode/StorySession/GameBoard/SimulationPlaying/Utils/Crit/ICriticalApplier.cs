using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils.Crit
{
    public interface ICriticalApplier
    {
        public float TryApply(float value, int index, int owner, BattleCache battleCache);
    }
}