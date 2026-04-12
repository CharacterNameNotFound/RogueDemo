using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public interface IItemTagToTableEntryConverter
    {
        public bool TryGetInsertObject(Item item, out object insert);
    }
}