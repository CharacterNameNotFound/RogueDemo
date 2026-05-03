using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles.Routines
{
    public interface IBattleEncounterRoutines
    {
        public UniTask ShowElements(BattleEncounter encounter, CancellationToken cancellationToken);

        public UniTask LoadItemsUpdateViews(IEnumerable<string> items, CancellationToken cancellationToken);
        public UniTask HideAll(CancellationToken cancellationToken);
    }
}