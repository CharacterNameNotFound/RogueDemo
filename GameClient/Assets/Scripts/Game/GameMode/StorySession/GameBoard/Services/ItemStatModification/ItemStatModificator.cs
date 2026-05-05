using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemStatModification
{
    public class ItemStatModificator : IItemStatModificator
    {
        public void SetStatBaseValue(
            float value,
            Item item, 
            ItemStatType itemStat,
            StatSet.StatSetComponent targetComponent = StatSet.StatSetComponent.PersistantBonus)
        {
            item.ItemStats.Stats[itemStat].ItemValues.Stats[(int)targetComponent] = value;
        }

        public void SetStatPercentilesValue(
            float value,
            Item item, 
            ItemStatType itemStat,
            StatSet.StatSetComponent targetComponent = StatSet.StatSetComponent.PersistantBonus)
        {
            item.ItemStats.Stats[itemStat].ItemPercentiles.Stats[(int)targetComponent] = value;
        }

        public void AddStatBaseValue(
            float value,
            Item item, 
            ItemStatType itemStat,
            StatSet.StatSetComponent targetComponent = StatSet.StatSetComponent.PersistantBonus)
        {
            item.ItemStats.Stats[itemStat].ItemValues.Stats[(int)targetComponent] += value;
        }

        /// <summary>
        /// Despite being called Add, operation is multiplication, so you need to pass 0.5 to clear 2
        /// </summary>
        public void AddStatPercentilesValue(
            float value,
            Item item, 
            ItemStatType itemStat,
            StatSet.StatSetComponent targetComponent = StatSet.StatSetComponent.PersistantBonus)
        {
            item.ItemStats.Stats[itemStat].ItemPercentiles.Stats[(int)targetComponent] *= value;
        }
    }
}