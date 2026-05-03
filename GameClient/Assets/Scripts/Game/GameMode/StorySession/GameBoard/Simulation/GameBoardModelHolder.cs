namespace Game.GameMode.StorySession.GameBoard.Simulation
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