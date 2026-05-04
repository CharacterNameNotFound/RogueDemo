using System.Collections.Generic;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public interface IStoryContext
    {
        // cycle, encounter set;
        // cycle <- day * day_length + hour
        public List<List<string>> StoryEncounters { get; set; }
    }
}