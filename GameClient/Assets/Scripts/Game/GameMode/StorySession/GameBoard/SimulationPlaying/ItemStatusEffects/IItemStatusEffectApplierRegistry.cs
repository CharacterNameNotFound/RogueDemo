using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public interface IItemStatusEffectApplierRegistry
    {
        /// <param name="key"> ItemStatusEffect type </param>
        public bool Get(Type key, out IItemStatusEffectApplier result);
    }
}