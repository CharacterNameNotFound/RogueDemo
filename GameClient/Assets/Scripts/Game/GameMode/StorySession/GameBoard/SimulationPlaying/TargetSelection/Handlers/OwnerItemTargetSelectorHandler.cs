using System;
using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding;
using Game.Utilities.Shuffling;
using GameWideSystems.RNGManagement;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection.Handlers
{
    public class OwnerItemTargetSelectorHandler : ITargetSelectionHandler
    {
        public Type AutoDictionaryKey => typeof(ITargetSelectionHandler);
        
        private IRNGManager _rngManager;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;

        public OwnerItemTargetSelectorHandler(IRNGManager rngManager, IStatProviderHandlersRegistry statProviderHandlersRegistry)
        {
            _rngManager = rngManager;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
        }

        // ToDo: optimize...
        public int[] GetTargetIndex(TargetSelector targetSelector, int index, int owner, BattleCache battleCache)
        { 
            EnemyItemTargetSelector selector = (EnemyItemTargetSelector) targetSelector;
            
            List<int> availableTargets = battleCache.Get(owner).ItemCache
                .Where(item => !item.Item.ItemStats.IsPassiveItem || selector.SelectNonCooldownItems)
                .Select(item => item.Index).ToList();
            
            availableTargets.ShuffleListDurstenfeld(_rngManager.GetRandomProvider(RNGGroup.Battle));

            int count = 1;
            
            if (_statProviderHandlersRegistry.Get(selector.TargetCount.GetType(), out IStatProvidingHandler statProvider))
            {
                count = Mathf.CeilToInt(statProvider.GetValue(selector.TargetCount, index, owner, battleCache));
            }

            count = Math.Min(count, availableTargets.Count);

            return availableTargets.Take(count).ToArray();
        }
        
    }
}