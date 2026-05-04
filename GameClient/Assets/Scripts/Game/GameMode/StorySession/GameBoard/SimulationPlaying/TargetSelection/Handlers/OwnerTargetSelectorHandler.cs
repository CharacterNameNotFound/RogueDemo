using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection.Handlers
{
    public class OwnerTargetSelectorHandler : ITargetSelectionHandler
    {
        public Type AutoDictionaryKey => typeof(OwnerTargetSelector);
        
        public int[] GetTargetIndex(TargetSelector targetSelector, int index, int owner, BattleCache battleCache)
        {
            int targetIndex = TargetCalculator.GetTargetId(owner, 1);
            
            return new []{targetIndex};
        }
    }
}