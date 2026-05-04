using Game.GameMode.StorySession.GameBoard.Services.TextsDrawing;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating
{
    public class PlayerStatusUpdater : IPlayerStatusUpdater
    {
        private ISessionStatusDrawer _sessionStatusDrawer;
        private IGameBoardModelHolder _gameBoardModelHolder;        

        public PlayerStatusUpdater(ISessionStatusDrawer sessionStatusDrawer, IGameBoardModelHolder gameBoardModelHolder)
        {
            _sessionStatusDrawer = sessionStatusDrawer;
            _gameBoardModelHolder = gameBoardModelHolder;
        }

        public void UpdateCoins(float value)
        {
            UpdateCoins(Mathf.CeilToInt(value));
        }

        public void UpdateCoins(int value)
        {
            _gameBoardModelHolder.GameBoardModel.PlayerStats.Coins += Mathf.CeilToInt(value);
            _sessionStatusDrawer.RedrawPlayerStats(_gameBoardModelHolder.GameBoardModel);
        }
        
    }
}