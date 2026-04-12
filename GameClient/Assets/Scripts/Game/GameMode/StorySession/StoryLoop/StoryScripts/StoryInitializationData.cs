using Game.GameMode.StorySession.Data.Character;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public class StoryInitializationData
    {
        public string CharacterId;

        public StoryInitializationData(string characterId)
        {
            CharacterId = characterId;
        }
    }
}