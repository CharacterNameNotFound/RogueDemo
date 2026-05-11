using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection.Handlers
{
    public class OwnerTargetSelectorHandler : ITargetSelectionHandler
    {
        public Type AutoDictionaryKey => typeof(OwnerTargetSelector);

        public (int[] itemIds, int targetHero) GetTargetIndex(TargetSelector targetSelector, int index, int owner, BattleCache battleCache)
        {
            return (Array.Empty<int>(), TargetCalculator.GetTargetId(owner, 0));
        }
    }
}