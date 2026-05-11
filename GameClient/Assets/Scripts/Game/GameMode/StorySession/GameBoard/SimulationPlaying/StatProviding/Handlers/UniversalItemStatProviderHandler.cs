using System;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatProviders;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;

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

            Item item = CacheShortcuts.GetItem(index, owner, battleCache);
            
            float value = _itemStatGetter.GetStatValue(item, universalItemStatProvider.ItemStatType) * universalItemStatProvider.Multiplier;
            
            return value;
        }
    }
}