using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;

namespace Game.GameMode.StorySession.StoryLoop.Encounters.Merchants.Utilities
{
    public interface IMerchantItemExclusionListBuilder
    {
        public UniTask<HashSet<string>> BuildIgnoredListIds(IStoryContext storyContext, CancellationToken cancellationToken);
    }
}