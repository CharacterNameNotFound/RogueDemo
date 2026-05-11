using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;

namespace Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters
{
    public interface IItemTagToTableEntryConverter
    {
        public bool TryGetInsertObject(Item item, out object insert);
    }
}