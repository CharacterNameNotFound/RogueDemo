using System;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatProviders;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding.Handlers
{
    public class UniversalItemStatProviderHandler : IStatProvidingHandler
    {
        public Type AutoDictionaryKey => typeof(UniversalItemStatProvider);

        private IItemStatGetter _itemStatGetter;

        public UniversalItemStatProviderHandler(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }

        public float GetValue(StatProvider statProvider, int index, int owner, BattleCache battleCache)
        {
            UniversalItemStatProvider universalItemStatProvider = (UniversalItemStatProvider) statProvider;

            float value = _itemStatGetter.GetStatValue(battleCache.Get(owner).Model.Items[index],
                universalItemStatProvider.ItemStatType) * universalItemStatProvider.Multiplier;
            
            return value;
        }
    }
}