using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.GameBoard.View;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.Services.TextsDrawing
{
    public class SessionStatusDrawer : ISessionStatusDrawer
    {
        private GameBoardHolder _gameBoardHolder;
        private ILocalizationManager _localizationManager;
        private SessionStatusDrawingConfigs _sessionStatusDrawingConfigs;

        private int _stepsInCycle;

        public SessionStatusDrawer(
            GameBoardHolder gameBoardHolder, 
            ILocalizationManager localizationManager, 
            SessionStatusDrawingConfigs sessionStatusDrawingConfigs)
        {
            _gameBoardHolder = gameBoardHolder;
            _localizationManager = localizationManager;
            _sessionStatusDrawingConfigs = sessionStatusDrawingConfigs;
        }

        public void Initialize(int stepsInCycle)
        {
            _stepsInCycle = stepsInCycle;
        }

        public void RedrawPlayerStats(GameBoardModel gameBoardModel)
        {
            string line = _localizationManager.GetLocalized(
                _sessionStatusDrawingConfigs.PlayerStatusText, 
                gameBoardModel.PlayerStats.Coins, 
                gameBoardModel.PlayerStats.Experience, 
                gameBoardModel.PlayerStats.ExperienceRequired);
            
            _gameBoardHolder.GameBoardComponent.GameBoardInteractables.PlayerStatsTextView.SetText(line);
        }

        public void RedrawStoryProgression(GameBoardModel gameBoardModel)
        {
            string line = _localizationManager.GetLocalized(
                _sessionStatusDrawingConfigs.CyclesText, 
                gameBoardModel.StoryStats.Cycle + 1, 
                gameBoardModel.StoryStats.Step + 1,
                _stepsInCycle);
            
            _gameBoardHolder.GameBoardComponent.GameBoardInteractables.GameCyclesTextView.SetText(line);
        }
    }
}