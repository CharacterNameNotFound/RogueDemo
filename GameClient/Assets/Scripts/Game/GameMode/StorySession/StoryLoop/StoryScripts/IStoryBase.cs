using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public interface IStoryBase
    {
        public UniTask StartStory(CancellationToken cancellationToken);
        public UniTask Load(CancellationToken cancellationToken);
        public UniTask Loop(CancellationToken cancellationToken);
        public UniTask Finish(CancellationToken cancellationToken);
        public UniTask CleanUp(CancellationToken cancellationToken);

    }
}