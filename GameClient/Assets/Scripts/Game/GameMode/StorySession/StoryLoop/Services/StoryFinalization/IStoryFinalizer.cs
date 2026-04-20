namespace Game.GameMode.StorySession.StoryLoop.Services.StoryFinalization
{
    public interface IStoryFinalizer
    {
        public void SetStoryFinalizationReady();
        public bool IsFinalizationRequested();
    }
}