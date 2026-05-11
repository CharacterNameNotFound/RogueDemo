using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public class SimulationModelUpdater : ISimulationModelUpdater
    {
        private IItemStatGetter _itemStatGetter;

        public SimulationModelUpdater(IItemStatGetter itemStatGetter)
        {
            _itemStatGetter = itemStatGetter;
        }


        public void ResetChargeValues(List<ItemCache> items)
        {
            foreach (ItemCache entry in items)
            {
                if (!entry.Item.ItemStats.IsPassiveItem)
                {
                    entry.Item.ItemStats.CurrentCharge = 0;
                    
                    continue;
                }

                entry.Item.ItemStats.IsCharged = false;
                entry.Item.ItemStats.CurrentCharge = 
                    _itemStatGetter.GetStatValue(entry.Item, ItemStatType.MaxCharge);
            }
        }

        public void ProgressCharge(List<ItemCache> items, TriggerBuffer triggerBuffer, float deltaTime)
        {
            foreach (ItemCache entry in items)
            {
                if (entry.Item.ItemStats.IsPassiveItem || entry.Item.ItemStats.IsCharged)
                {
                    continue;
                }

                float chargeSpeed = _itemStatGetter.GetStatValue(entry.Item, ItemStatType.ChargeSpeed);
                float maxCharge = _itemStatGetter.GetStatValue(entry.Item, ItemStatType.MaxCharge);

                entry.Item.ItemStats.CurrentCharge += chargeSpeed * deltaTime;

                if (entry.Item.ItemStats.CurrentCharge >= maxCharge)
                {
                    entry.Item.ItemStats.IsCharged = true;
                    triggerBuffer.AddTrigger(entry.ToItemCooldownTrigger());
                }
                
            }
        }
    }
}