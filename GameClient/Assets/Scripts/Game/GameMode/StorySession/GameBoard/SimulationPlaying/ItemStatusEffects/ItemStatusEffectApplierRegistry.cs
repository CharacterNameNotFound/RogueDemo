using System;
using GameWideSystems.Logger;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public class ItemStatusEffectApplierRegistry : AutoDictionary<Type, IItemStatusEffectApplier>, IItemStatusEffectApplierRegistry
    {
        private Logger _logger;
        
        public ItemStatusEffectApplierRegistry(IItemStatusEffectApplier[] entries, Logger logger) : base(entries)
        {
            _logger = logger;
        }

        public bool Get(Type key, out IItemStatusEffectApplier result)
        {
            if (TryGet(key, out result)) 
                return true;
            
            _logger.Warn($"Item status applier of type {key.Name} is missing");
            return false;
        }
    }
}