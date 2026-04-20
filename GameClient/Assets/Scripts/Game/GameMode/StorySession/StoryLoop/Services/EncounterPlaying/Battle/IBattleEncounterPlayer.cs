using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Battle
{
    public interface IBattleEncounterPlayer
    {
        public UniTask Play(Encounter encounter, IStoryContext storyContext, CancellationToken cancellationToken);
    }
}