using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying
{
    public interface IEncounterPlayer
    {
        public UniTask PlayEncounter(string encounterId, IStoryContext storyContext, CancellationToken cancellationToken);
    }
}