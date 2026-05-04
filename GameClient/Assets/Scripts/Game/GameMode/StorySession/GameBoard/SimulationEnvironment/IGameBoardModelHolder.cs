namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment
{
    public interface IGameBoardModelHolder
    {
        public GameBoardModel GameBoardModel { get; }

        public void Set(GameBoardModel gameBoardModel);

    }
}