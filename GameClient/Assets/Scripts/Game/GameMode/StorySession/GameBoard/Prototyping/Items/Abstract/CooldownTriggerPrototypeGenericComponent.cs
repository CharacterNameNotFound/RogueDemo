using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Triggers;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract
{
    public abstract class CooldownTriggerPrototypeGenericComponent<T> : ItemComponentPrototypeGenericComponent<T> where T : TriggerComponent
    {
        public override void WriteToItem(Item item)
        {
            item.CooldownTriggers.Add((T) Value.GetCopy());
        }
    }

}