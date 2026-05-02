using Game.GameMode.StorySession.GameBoard.Simulation.Player;

namespace Game.GameMode.StorySession.GameBoard.Simulation
{
    public class GameBoardModel
    {
        public ItemBoardModel PlayerBoard = new();
        public ItemBoardModel PlayerStashBoard = new();
        public ItemBoardModel EncounterBoard = new();

        public PlayerStats PlayerStats = new();
        
        public int Cycle = 0;
        public int Step = 0;
    }
}