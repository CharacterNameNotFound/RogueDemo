namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment
{
    public class GameBoardModelHolder : IGameBoardModelHolder
    {
        public GameBoardModel GameBoardModel { get; private set; }
        
        public void Set(GameBoardModel gameBoardModel)
        {
            GameBoardModel = gameBoardModel;
        }
        
    }
}