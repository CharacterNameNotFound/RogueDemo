using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public class WinDecisionMaker : IWinDecisionMaker
    {
        private IGameBoardModelHolder _gameBoardModelHolder;

        public WinDecisionMaker(IGameBoardModelHolder gameBoardModelHolder)
        {
            _gameBoardModelHolder = gameBoardModelHolder;
        }

        public bool IsWinConditionReached()
        {
            return _gameBoardModelHolder.GameBoardModel.PlayerHeroStats.Hp <= 0 || 
                   _gameBoardModelHolder.GameBoardModel.EncounterHeroStats.Hp <= 0;
        }
        
        
    }
}