using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public interface IItemStatusEffectApplier : IAutoDictionaryEntry<Type>
    {
        // Returns true if effect was applied, return false if effect was updated
        public bool Apply(Item item, float duration);
        public bool Remove(Item item);
    }
}