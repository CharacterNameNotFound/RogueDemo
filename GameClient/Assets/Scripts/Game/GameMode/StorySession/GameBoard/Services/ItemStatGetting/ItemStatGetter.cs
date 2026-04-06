using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting
{
    public class ItemStatGetter : IItemStatGetter
    {
        private GenericStatCalculator _genericStatCalculator;
        
        private Dictionary<ItemStatType, IItemStatSetToItemStatValueCalculator> _statCalculators;

        public ItemStatGetter(
            IItemStatSetToItemStatValueCalculator[] statCalculators, 
            GenericStatCalculator genericStatCalculator)
        {
            _genericStatCalculator = genericStatCalculator;
            _statCalculators = statCalculators.ToDictionary(item => item.ProcessedStat);
        }

        public float GetStatValue(
            Item item, 
            ItemStatType itemStat, 
            StatSet.StatSetComponent baseCalculateDepth, 
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {
            return _statCalculators
                .GetValueOrDefault(itemStat, _genericStatCalculator)
                .GetValue(item, baseCalculateDepth, multiplicationCalculateDepth);
        }
    }
}