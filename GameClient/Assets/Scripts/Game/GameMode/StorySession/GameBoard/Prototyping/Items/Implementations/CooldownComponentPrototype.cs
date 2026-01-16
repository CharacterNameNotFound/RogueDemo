using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Implementations
{
    public class CooldownComponentPrototype : ItemComponentPrototypeGenericComponent<CooldownComponent>
    {
        public override void WriteToItem(Item item)
        {
            item.CooldownComponent = (CooldownComponent) Value.GetCopy();
        }
    }
}