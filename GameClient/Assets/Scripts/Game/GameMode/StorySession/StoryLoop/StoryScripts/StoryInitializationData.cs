using Game.GameMode.StorySession.Data.Character;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public class StoryInitializationData
    {
        public CharacterData CharacterData;

        public StoryInitializationData(CharacterData characterData)
        {
            CharacterData = characterData;
        }
    }
}