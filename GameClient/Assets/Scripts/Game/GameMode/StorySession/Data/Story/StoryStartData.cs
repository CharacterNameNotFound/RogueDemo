namespace Game.GameMode.StorySession.Data.Story
{
    public class StoryStartData
    {
        public string StoryID;
        public string CharacterID;

        public StoryStartData(string storyID, string characterID)
        {
            StoryID = storyID;
            CharacterID = characterID;
        }
    }
}