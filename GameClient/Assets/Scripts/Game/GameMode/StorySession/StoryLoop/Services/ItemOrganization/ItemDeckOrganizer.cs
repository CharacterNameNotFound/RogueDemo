using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.Utilities;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemDeckOrganizer : DeckOrganizer<ItemRarity, string>
    {
        public ItemDeckOrganizer(IRNGManager rngManager) : base(rngManager)
        {
        }
        
    }
}