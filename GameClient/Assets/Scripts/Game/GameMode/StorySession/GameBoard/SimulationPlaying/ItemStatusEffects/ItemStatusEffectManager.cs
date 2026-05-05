using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
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

        public UniTask PostBattleReset(BattleCache battleCache, CancellationToken cancellationToken)
        {
            List<ItemCache> playerSide = battleCache.GetPlayer().ItemCache;
            List<ItemCache> encounterSide = battleCache.GetEncounter().ItemCache;

            foreach (ItemCache item in playerSide)
            {
                ClearItem(item.Item);
            }
            
            foreach (ItemCache item in encounterSide)
            {
                ClearItem(item.Item);
            }

            return UniTask.CompletedTask;
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
        
        private void ClearItem(Item item)
        {
            // ToDo: optimize
            foreach (IItemStatusEffect statusEffect in item.StatusEffects.Values.ToArray())
            {
                _applierRegistry.Get(statusEffect.GetType(), out IItemStatusEffectApplier applier);
                
                applier.Remove(item);
            }
            
            
        }


    }
}