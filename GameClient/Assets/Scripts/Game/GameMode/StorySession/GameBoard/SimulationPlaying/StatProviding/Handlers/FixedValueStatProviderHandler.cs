using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatProviders;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding.Handlers
{
    public class FixedValueStatProviderHandler : IStatProvidingHandler
    {
        public Type AutoDictionaryKey => typeof(FixedValueStatProvider);
        
        public float GetValue(StatProvider statProvider, int index, int owner, BattleCache battleCache)
        {
            return ((FixedValueStatProvider) statProvider).Value;
        }
    }
}