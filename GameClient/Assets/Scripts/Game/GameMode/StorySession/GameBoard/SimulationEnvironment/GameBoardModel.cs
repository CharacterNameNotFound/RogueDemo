using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment
{
    public class GameBoardModel
    {
        public ItemBoardModel PlayerBoard;
        public ItemBoardModel PlayerStashBoard;
        public ItemBoardModel EncounterBoard;

        public HeroStats PlayerHeroStats;
        public HeroStats EncounterHeroStats;

        public PlayerStats PlayerStats;

        public StoryStats StoryStats;

        public GameBoardModel(
            ItemBoardModel playerBoard, 
            ItemBoardModel playerStashBoard, 
            ItemBoardModel encounterBoard, 
            PlayerStats playerStats, 
            StoryStats storyStats, 
            HeroStats playerHeroStats, 
            HeroStats encounterHeroStats)
        {
            PlayerBoard = playerBoard;
            PlayerStashBoard = playerStashBoard;
            EncounterBoard = encounterBoard;
            PlayerStats = playerStats;
            StoryStats = storyStats;
            PlayerHeroStats = playerHeroStats;
            EncounterHeroStats = encounterHeroStats;
        }
        
    }
}