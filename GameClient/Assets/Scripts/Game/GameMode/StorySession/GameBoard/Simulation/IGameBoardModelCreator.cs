using Game.GameMode.StorySession.Data.Character;

namespace Game.GameMode.StorySession.GameBoard.Simulation
{
    public interface IGameBoardModelCreator
    {
        public GameBoardModel CrateNew(GameBoardModelCreationConfigs gameBoardModelCreationConfigs, CharacterData characterData);
    }
}