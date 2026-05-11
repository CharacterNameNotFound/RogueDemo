using System;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatModification;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects.StatusEffectAppliers
{
    public class HasteItemStatusEffectApplier : IItemStatusEffectApplier
    {
        public Type AutoDictionaryKey => typeof(HasteItemStatusEffect);

        private IItemStatModificator _itemStatModificator;

        public HasteItemStatusEffectApplier(IItemStatModificator itemStatModificator)
        {
            _itemStatModificator = itemStatModificator;
        }

        public void Apply(Item item, float duration)
        {
            if (item.StatusEffects.TryGetValue(typeof(SlowItemStatusEffect), out IItemStatusEffect value))
            {
                ((HasteItemStatusEffect)value).Duration -= duration;
                return;
            }
            
            if (item.StatusEffects.TryGetValue(typeof(HasteItemStatusEffect), out value))
            {
                ((HasteItemStatusEffect)value).Duration += duration;
                return;
            }

            HasteItemStatusEffect effect = new HasteItemStatusEffect(duration);
            item.StatusEffects.Add(typeof(HasteItemStatusEffect), effect);

            // ToDo: rewire working on curses 
            _itemStatModificator.AddStatPercentilesValue(2, item, ItemStatType.ChargeSpeed, StatSet.StatSetComponent.CombatBonus);
            
        }

        public void Remove(Item item)
        {
            // in the future we will store effect power here
            if (!item.StatusEffects.Remove(typeof(HasteItemStatusEffect), out IItemStatusEffect value))
            {
                return;
            }
            
            _itemStatModificator.AddStatPercentilesValue(0.5f, item, ItemStatType.ChargeSpeed, StatSet.StatSetComponent.CombatBonus);
        }
        
    }
}