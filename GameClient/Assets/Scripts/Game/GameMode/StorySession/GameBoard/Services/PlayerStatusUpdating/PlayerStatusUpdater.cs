using Game.GameMode.StorySession.GameBoard.Services.TextsDrawing;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating
{
    public class PlayerStatusUpdater : IPlayerStatusUpdater
    {
        private ISessionStatusDrawer _sessionStatusDrawer;
        private IStoryContextProvider _storyContextProvider;

        public PlayerStatusUpdater(ISessionStatusDrawer sessionStatusDrawer, IStoryContextProvider storyContextProvider)
        {
            _sessionStatusDrawer = sessionStatusDrawer;
            _storyContextProvider = storyContextProvider;
        }

        public void UpdateCoins(float value)
        {
            UpdateCoins(Mathf.CeilToInt(value));
        }

        public void UpdateCoins(int value)
        {
            _storyContextProvider.StoryContext.GameBoardModel.PlayerStats.Coins += Mathf.CeilToInt(value);
            _sessionStatusDrawer.RedrawPlayerStats(_storyContextProvider.StoryContext.GameBoardModel);
        }
        
    }
}