using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.Data.Story
{
    public class StoryStartData
    {
        public AssetReference StoryID;
        public string CharacterID;

        public StoryStartData(AssetReference storyID, string characterID)
        {
            StoryID = storyID;
            CharacterID = characterID;
        }
    }
}