using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant
{
    public interface IMerchantEncounterRoutines
    {
        public UniTask ShowElements(MerchantEncounter encounter, IStoryContext storyContext, CancellationToken cancellationToken);

        public UniTask ShowWares(IEnumerable<string> items, IStoryContext storyContext, CancellationToken cancellationToken);
        public UniTask HideAll(CancellationToken cancellationToken);


    }
}