using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection.Handlers
{
    public class TriggeredItemTargetSelectorHandler : ITargetSelectionHandler
    {
        public Type AutoDictionaryKey => typeof(TriggeredItemTargetSelector);

        public (int[] itemIds, int targetHero) GetTargetIndex(TargetSelector targetSelector, int index, int owner,
            BattleCache battleCache)
        {
            return (new []{index}, owner);
        }
    }
}