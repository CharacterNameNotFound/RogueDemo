using Game.GameMode.StorySession.GameBoard.Simulation;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs
{
    public interface IStoryGenericConfigs
    {
        public int StoryDayLength { get; }
        public int StoryDayCount { get; }
        public GameBoardModelCreationConfigs GameBoardModelCreationConfigs { get; }
    }
}