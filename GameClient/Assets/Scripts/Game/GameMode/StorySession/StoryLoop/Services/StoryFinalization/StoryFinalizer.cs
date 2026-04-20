namespace Game.GameMode.StorySession.StoryLoop.Services.StoryFinalization
{
    public class StoryFinalizer : IStoryFinalizer
    {
        private bool _isReadyToFinalize;
        
        public void SetStoryFinalizationReady()
        {
            _isReadyToFinalize = true;
        }

        public bool IsFinalizationRequested()
        {
            return _isReadyToFinalize;
        }
    }
}