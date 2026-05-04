using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;

namespace Game.GameMode.StorySession.GameBoard.Services.TextsDrawing
{
    public interface ISessionStatusDrawer
    {
        public void Initialize(int stepsInCycle);
        public void RedrawPlayerStats(GameBoardModel gameBoardModel);
        public void RedrawStoryProgression(GameBoardModel gameBoardModel);
    }
}