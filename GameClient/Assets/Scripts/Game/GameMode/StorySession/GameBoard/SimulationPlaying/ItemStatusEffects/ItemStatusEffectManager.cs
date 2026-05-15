using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects.ItemStatusEffectVFXApplication;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public class ItemStatusEffectManager : IItemStatusEffectManager
    {
        private IItemStatusEffectApplierRegistry _applierRegistry;
        private IItemStatusEffectVFXApplier _statusEffectVFXApplier; 


        public ItemStatusEffectManager(
            IItemStatusEffectApplierRegistry applierRegistry, 
            IItemStatusEffectVFXApplier statusEffectVFXApplier)
        {
            _applierRegistry = applierRegistry;
            _statusEffectVFXApplier = statusEffectVFXApplier;
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
                ClearItem(item);
            }
            
            foreach (ItemCache item in encounterSide)
            {
                ClearItem(item);
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
                
                bool isRemoved = applier.Remove(item.Item);

                if (isRemoved)
                {
                    _statusEffectVFXApplier.RemoveItemFrameParticles(applier.AutoDictionaryKey, item.Index, item.OwnerIndex);
                }
                
            }
            
            
        }
        
        private void ClearItem(ItemCache item)
        {
            // ToDo: optimize
            foreach (IItemStatusEffect statusEffect in item.Item.StatusEffects.Values.ToArray())
            {
                _applierRegistry.Get(statusEffect.GetType(), out IItemStatusEffectApplier applier);
                
                bool isRemoved = applier.Remove(item.Item);
                
                if (isRemoved)
                {
                    _statusEffectVFXApplier.RemoveItemFrameParticles(applier.AutoDictionaryKey, item.Index, item.OwnerIndex);
                }
            }
            
            
        }


    }
}