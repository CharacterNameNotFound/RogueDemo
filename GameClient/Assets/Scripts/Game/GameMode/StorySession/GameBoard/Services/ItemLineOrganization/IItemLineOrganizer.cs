using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    public interface IItemLineOrganizer
    {
        public void Organize(ItemLineComponent itemLineComponent, ItemContainerComponent[] itemConfiguration, bool updatedStored);
        public void Restore(ItemLineComponent itemLineComponent);
        
    }
}