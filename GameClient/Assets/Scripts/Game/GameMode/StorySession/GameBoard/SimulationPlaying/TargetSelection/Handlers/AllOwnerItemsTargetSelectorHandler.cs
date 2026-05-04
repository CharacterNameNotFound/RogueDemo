using System;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.TargetSelectors;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection.Handlers
{
    public class AllOwnerItemsTargetSelectorHandler : ITargetSelectionHandler
    {
        public Type AutoDictionaryKey => typeof(AllOwnerItemsTargetSelector);
        
        public int[] GetTargetIndex(TargetSelector targetSelector, int index, int owner, BattleCache battleCache)
        {
            AllEnemyItemsTargetSelector selector = (AllEnemyItemsTargetSelector)targetSelector;

            return battleCache.Get(owner).ItemCache
                .Where(item => !item.Item.ItemStats.IsPassiveItem || selector.SelectNonCooldownItems)
                .Select(item => item.Index).ToArray();
        }
    }
}