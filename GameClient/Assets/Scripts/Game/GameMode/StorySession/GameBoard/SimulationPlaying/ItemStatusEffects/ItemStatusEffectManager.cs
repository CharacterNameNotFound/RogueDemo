using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public class ItemStatusEffectManager : IItemStatusEffectManager
    {
        private IItemStatusEffectApplierRegistry _applierRegistry;

        public ItemStatusEffectManager(IItemStatusEffectApplierRegistry applierRegistry)
        {
            _applierRegistry = applierRegistry;
        }

        public void Update(List<ItemCache> items, float deltaTime)
        {
            foreach (ItemCache item in items)
            {
                UpdatedItem(item, deltaTime);
            }
        }

        private void UpdatedItem(ItemCache item, float deltaTime)
        {
            // ToDo: optimize
            foreach (IItemStatusEffect statusEffect in item.Item.StatusEffects.Values.ToList())
            {
                statusEffect.Update(deltaTime);
                if (!statusEffect.IsReadyToRemove())
                {
                    continue;
                }

                _applierRegistry.Get(statusEffect.GetType(), out IItemStatusEffectApplier applier);
                
                applier.Remove(item.Item);
            }
            
            
        }


    }
}