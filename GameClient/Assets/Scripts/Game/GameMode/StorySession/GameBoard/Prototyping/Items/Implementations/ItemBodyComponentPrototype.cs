using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Implementations
{
    public class ItemBodyComponentPrototype : ItemComponentPrototypeGenericComponent<ItemBodyComponent>
    {
        public override void WriteToItem(Item item)
        {
            item.ItemBody = (ItemBodyComponent) Value.GetCopy();
        }
    }
}