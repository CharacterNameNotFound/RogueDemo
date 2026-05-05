using System;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatModification;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects.StatusEffectAppliers
{
    public class SlowItemStatusEffectApplier : IItemStatusEffectApplier
    {
        public Type AutoDictionaryKey => typeof(SlowItemStatusEffect);

        private IItemStatModificator _itemStatModificator;

        public SlowItemStatusEffectApplier(IItemStatModificator itemStatModificator)
        {
            _itemStatModificator = itemStatModificator;
        }

        public void Apply(Item item, float duration)
        {
            if (item.StatusEffects.TryGetValue(typeof(HasteItemStatusEffect), out IItemStatusEffect value))
            {
                ((HasteItemStatusEffect)value).Duration -= duration;
                return;
            }
            
            if (item.StatusEffects.TryGetValue(typeof(SlowItemStatusEffect), out value))
            {
                ((SlowItemStatusEffect)value).Duration += duration;
                return;
            }

            SlowItemStatusEffect effect = new SlowItemStatusEffect(duration);
            item.StatusEffects.Add(typeof(SlowItemStatusEffect), effect);

            // ToDo: rewire working on curses 
            _itemStatModificator.AddStatPercentilesValue(0.5f, item, ItemStatType.ChargeSpeed, StatSet.StatSetComponent.CombatBonus);
            
        }

        public void Remove(Item item)
        {
            // in the future we will store effect power here
            if (!item.StatusEffects.Remove(typeof(SlowItemStatusEffect), out IItemStatusEffect value))
            {
                return;
            }
            
            _itemStatModificator.AddStatPercentilesValue(2f, item, ItemStatType.ChargeSpeed, StatSet.StatSetComponent.CombatBonus);
        }
    }
}