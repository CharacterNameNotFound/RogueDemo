using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding
{
    public interface IStatProvidingHandler : IAutoDictionaryEntry<Type>
    {
        public float GetValue(StatProvider statProvider, int index, int owner, BattleCache battleCache);
    }
}