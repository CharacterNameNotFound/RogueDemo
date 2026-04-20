using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection
{
    public interface IEncounterSelector
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public UniTask<int> StartEncounterSelection(List<string> encounterIds, CancellationToken cancellationToken);
        public void CleanUp(CancellationToken cancellationToken);

    }
}