using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects
{
    public interface IItemStatusEffectApplier : IAutoDictionaryEntry<Type>
    {
        public void Apply(Item item, float duration);
        public void Remove(Item item);
    }
}