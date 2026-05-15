using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Gifts.Routines
{
    public interface IGiftEncounterRoutines
    {
        public UniTask ShowElements(GiftEncounter encounter, CancellationToken cancellationToken);

        public UniTask ShowWares(IEnumerable<string> items, CancellationToken cancellationToken);
        public UniTask HideAll(CancellationToken cancellationToken);
    }
}