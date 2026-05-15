using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Utilities
{
    public interface IObtainableItemExclusionListBuilder
    {
        public UniTask<HashSet<string>> BuildIgnoredListIds(GameBoardModel gameBoardModel, CancellationToken cancellationToken);
        public UniTask AppendItemExclusion(string item, HashSet<string> container, CancellationToken cancellationToken);
    }
}