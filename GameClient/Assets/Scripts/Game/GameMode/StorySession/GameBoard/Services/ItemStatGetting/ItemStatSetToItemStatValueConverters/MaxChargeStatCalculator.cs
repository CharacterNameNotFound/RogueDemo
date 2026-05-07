using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters
{
    public class MaxChargeStatCalculator : IItemStatSetToItemStatValueCalculator
    {
        public ItemStatType ProcessedStat => ItemStatType.MaxCharge;
        
        public float GetValue(
            Item item, 
            StatSet.StatSetComponent baseCalculateDepth, 
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {
            if (!item.ItemStats.Stats.TryGetValue(ProcessedStat, out ItemStatEntry statEntry))
            {
                return 0;
            }

            float statValue = Calculate(statEntry, baseCalculateDepth, multiplicationCalculateDepth);
            return Mathf.Max(0, statValue);
        }
        
        public float GetValue(
            ItemStatEntry statEntry, 
            StatSet.StatSetComponent baseCalculateDepth, 
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {
            float statValue = Calculate(statEntry, baseCalculateDepth, multiplicationCalculateDepth);
            return Mathf.Max(0, statValue);
        }

        private float Calculate(
            ItemStatEntry itemValues, 
            StatSet.StatSetComponent baseCalculateDepth, 
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {
            float value = 0;
            float mult = 1;
            
            for (int i = 0; i <= (int) baseCalculateDepth; i++)
            {
                value += itemValues.ItemValues.Stats[i];
            }

            for (int i = 0; i <= (int) multiplicationCalculateDepth; i++)
            {
                mult *= itemValues.ItemPercentiles.Stats[i];
            }
            
            return Mathf.Max(1, value * mult);
        }
        
    }
}