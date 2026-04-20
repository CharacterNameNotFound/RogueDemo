using Game.GameMode.StorySession.Data.Story;
using GameWideSystems.GameStateManagement;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameModeInitializationParameters : GameStateInitializationParameters
    {
        public StoryStartData StoryStartData;
        public bool TryReadSaveFile;

        public StorySessionGameModeInitializationParameters(StoryStartData storyStartData, bool tryReadSaveFile)
        {
            StoryStartData = storyStartData;
            TryReadSaveFile = tryReadSaveFile;
        }
    }
}