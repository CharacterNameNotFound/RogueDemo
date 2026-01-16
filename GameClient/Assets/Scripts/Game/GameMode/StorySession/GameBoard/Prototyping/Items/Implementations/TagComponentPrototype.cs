using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Tags;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Implementations
{
    public class TagComponentPrototype : ItemComponentPrototypeGenericComponent<TagComponent>
    {
        public override void WriteToItem(Item item)
        {
            item.Tags = (TagComponent) Value.GetCopy();
        }
    }
}