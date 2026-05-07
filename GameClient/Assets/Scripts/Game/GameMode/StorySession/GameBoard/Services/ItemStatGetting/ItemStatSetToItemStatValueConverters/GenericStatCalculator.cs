using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters
{
    public class GenericStatCalculator
    {
        
        public float GetValue(
            Item item, 
            ItemStatType statType,
            StatSet.StatSetComponent baseCalculateDepth, 
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {
            if (!item.ItemStats.Stats.TryGetValue(statType, out ItemStatEntry statEntry))
            {
                return 0;
            }

            float statValue = CalculateGeneric(statEntry, baseCalculateDepth, multiplicationCalculateDepth);
            return Mathf.Max(0, statValue);
        }
        
        public float GetValue(
            ItemStatEntry statSet, 
            StatSet.StatSetComponent baseCalculateDepth, 
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {

            float statValue = CalculateGeneric(statSet, baseCalculateDepth, multiplicationCalculateDepth);
            return Mathf.Max(0, statValue);
        }

        private float CalculateGeneric(
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