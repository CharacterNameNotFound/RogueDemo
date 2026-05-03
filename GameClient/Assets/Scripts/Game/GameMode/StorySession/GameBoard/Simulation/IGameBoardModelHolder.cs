namespace Game.GameMode.StorySession.GameBoard.Simulation
{
    public interface IGameBoardModelHolder
    {
        public GameBoardModel GameBoardModel { get; }

        public void Set(GameBoardModel gameBoardModel);

    }
}