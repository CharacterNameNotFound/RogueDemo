using System;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection.Handlers
{
    public class AllEnemyItemsTargetSelectorHandler : ITargetSelectionHandler
    {
        public Type AutoDictionaryKey => typeof(AllEnemyItemsTargetSelector);
        
        public (int[] itemIds, int targetHero) GetTargetIndex(TargetSelector targetSelector, int index, int owner, BattleCache battleCache)
        {
            int targetId = TargetCalculator.GetTargetId(owner, 1);

            AllEnemyItemsTargetSelector selector = (AllEnemyItemsTargetSelector)targetSelector;

            return (battleCache.Get(targetId).ItemCache
                .Where(item => !item.Item.ItemStats.IsPassiveItem || selector.SelectNonCooldownItems)
                .Select(item => item.Index).ToArray(), 
                targetId);
        }
        
    }
}