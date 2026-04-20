using Game.GameMode.StorySession.GameBoard.Simulation.Player;

namespace Game.GameMode.StorySession.GameBoard.Simulation
{
    public class GameBoardModel
    {
        public ItemBoardModel PlayerBoard;
        public ItemBoardModel PlayerStashBoard;
        public ItemBoardModel EncounterBoard;

        public PlayerStats PlayerStats = new();

    }
}