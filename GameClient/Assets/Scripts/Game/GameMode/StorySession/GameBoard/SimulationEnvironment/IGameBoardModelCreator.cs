using Game.GameMode.StorySession.Data.Character;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment
{
    public interface IGameBoardModelCreator
    {
        public GameBoardModel CrateNew(GameBoardModelCreationConfigs gameBoardModelCreationConfigs, CharacterData characterData);
    }
}