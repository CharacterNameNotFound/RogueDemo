using Game.GameMode.StorySession.Data.Character;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public class StoryInitializationData
    {
        public string CharacterId;
        public bool TryReadSaveFile;
        
        
        public StoryInitializationData(string characterId, bool tryReadSaveFile)
        {
            CharacterId = characterId;
            TryReadSaveFile = tryReadSaveFile;
        }
    }
}