namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public class StoryContextHolder : IStoryContextProvider
    {
        public IStoryContext StoryContext { get; private set; }

        public void Set(IStoryContext storyContext)
        {
            StoryContext = storyContext;
        }

        public void Clear()
        {
            StoryContext = null;
        }
        
    }
}