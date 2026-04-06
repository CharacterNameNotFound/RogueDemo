using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting.ItemStatSetToItemStatValueConverters
{
    public class MaxChargeStatCalculator : GenericStatCalculator
    {
        public ItemStatType ProcessedStat => ItemStatType.MaxCharge;

        protected override float CalculateGeneric(ItemStatEntry itemValues, StatSet.StatSetComponent baseCalculateDepth,
            StatSet.StatSetComponent multiplicationCalculateDepth)
        {
            float value =  base.CalculateGeneric(itemValues, baseCalculateDepth, multiplicationCalculateDepth);

            return Mathf.Max(1, value);
        }
    }
}