using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters
{
    public class GenericStatCalculator : IItemStatSetToItemStatValueCalculator
    {
        public virtual ItemStatType ProcessedStat { get; }
        
        public float GetValue(
            Item item, 
            StatSet.StatSetComponent baseCalculateDepth, 
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {
            if (!item.ItemStats.Stats.TryGetValue(ProcessedStat, out ItemStatEntry statEntry))
            {
                return 0;
            }

            float statValue = CalculateGeneric(statEntry, baseCalculateDepth, multiplicationCalculateDepth);
            return Mathf.Max(0, statValue);
        }

        protected virtual float CalculateGeneric(
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
            
            return value * mult;
        }
        
    }
}