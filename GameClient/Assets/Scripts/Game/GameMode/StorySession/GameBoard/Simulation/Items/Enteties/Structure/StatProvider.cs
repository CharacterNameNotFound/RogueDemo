using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure
{
    public abstract class StatProvider
    {
        public abstract StatProvider GetCopy();
        public abstract float GetValue(Item item, IItemStatGetter statGetter);

    }
}