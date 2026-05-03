using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.Utilities
{
    public interface IMerchantItemExclusionListBuilder
    {
        public UniTask<HashSet<string>> BuildIgnoredListIds(GameBoardModel gameBoardModel, CancellationToken cancellationToken);
    }
}