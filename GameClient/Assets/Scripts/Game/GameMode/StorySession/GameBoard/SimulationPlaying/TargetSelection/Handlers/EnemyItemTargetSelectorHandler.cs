using System;
using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.Utilities.Shuffling;
using GameWideSystems.RNGManagement;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection.Handlers
{
    public class EnemyItemTargetSelectorHandler : ITargetSelectionHandler
    {
        public Type AutoDictionaryKey => typeof(EnemyItemTargetSelector);

        private IRNGManager _rngManager;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;

        public EnemyItemTargetSelectorHandler(IRNGManager rngManager, IStatProviderHandlersRegistry statProviderHandlersRegistry)
        {
            _rngManager = rngManager;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
        }

        // ToDo: optimize...
        public (int[] itemIds, int targetHero) GetTargetIndex(TargetSelector targetSelector, int index, int owner, BattleCache battleCache)
        {
            int targetId = TargetCalculator.GetTargetId(owner, 1);

            EnemyItemTargetSelector selector = (EnemyItemTargetSelector) targetSelector;
            
            List<int> availableTargets = battleCache.Get(targetId).ItemCache
                .Where(item => !item.Item.ItemStats.IsPassiveItem || selector.SelectNonCooldownItems)
                .Select(item => item.Index).ToList();
            
            availableTargets.ShuffleListDurstenfeld(_rngManager.GetRandomProvider(RNGGroup.Battle));

            int count = 1;
            
            if (_statProviderHandlersRegistry.Get(selector.TargetCount.GetType(), out IStatProvidingHandler statProvider))
            {
                count = Mathf.CeilToInt(statProvider.GetValue(selector.TargetCount, index, owner, battleCache));
            }

            count = Math.Min(count, availableTargets.Count);

            return (availableTargets.Take(count).ToArray(), targetId);
        }
    }
}