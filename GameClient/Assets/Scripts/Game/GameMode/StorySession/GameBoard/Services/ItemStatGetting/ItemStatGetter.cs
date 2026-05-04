using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

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
            if (_statCalculators.TryGetValue(itemStat, out IItemStatSetToItemStatValueCalculator statGetter))
            {
                statGetter.GetValue(item, baseCalculateDepth, multiplicationCalculateDepth);
            }

            return _genericStatCalculator.GetValue(item, itemStat, baseCalculateDepth, multiplicationCalculateDepth);

        }
    }
}