using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection
{
    public interface ITargetSelectionHandler : IAutoDictionaryEntry<Type>
    {
        public int[] GetTargetIndex(TargetSelector targetSelector, int index, int owner, BattleCache battleCache);
    }
}