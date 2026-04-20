namespace Game.GameMode.StorySession.Data.Story
{
    public class StoryStartData
    {
        // doubles as addressable key
        public string StoryID;
        public string CharacterID;

        public StoryStartData(string storyID, string characterID)
        {
            StoryID = storyID;
            CharacterID = characterID;
        }
    }
}