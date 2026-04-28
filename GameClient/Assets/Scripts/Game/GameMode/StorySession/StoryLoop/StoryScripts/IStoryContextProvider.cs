namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public interface IStoryContextProvider
    {
        public IStoryContext StoryContext { get; }
        public void Set(IStoryContext storyContext);
        public void Clear();
    }
}