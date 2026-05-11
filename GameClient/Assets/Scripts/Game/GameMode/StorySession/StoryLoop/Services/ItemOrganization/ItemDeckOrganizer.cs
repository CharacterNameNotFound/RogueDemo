using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.Utilities;
using Game.GameMode.StorySession.Utilities.Decks;
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