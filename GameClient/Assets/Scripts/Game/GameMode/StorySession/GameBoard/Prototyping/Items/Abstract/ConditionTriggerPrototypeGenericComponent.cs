using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Triggers;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract
{
    public abstract class ConditionTriggerPrototypeGenericComponent<T> : ItemComponentPrototypeGenericComponent<T> where T : TriggerComponent
    {
        public override void WriteToItem(Item item)
        {
            item.ConditionTriggers.Add((T) Value.GetCopy());
        }
    }
}