using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Routines.Merchant
{
    public interface IMerchantEncounterRoutines
    {
        public UniTask ShowElements(MerchantEncounter encounter, CancellationToken cancellationToken);

        public UniTask ShowWares(IEnumerable<string> items, CancellationToken cancellationToken);
        public UniTask HideAll(CancellationToken cancellationToken);


    }
}