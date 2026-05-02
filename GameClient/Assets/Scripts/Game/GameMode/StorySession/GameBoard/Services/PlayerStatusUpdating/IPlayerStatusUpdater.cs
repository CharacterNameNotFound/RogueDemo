namespace Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating
{
    public interface IPlayerStatusUpdater
    {
        public void UpdateCoins(float value);
        public void UpdateCoins(int value);
    }
}