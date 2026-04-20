using Game.GameMode.StorySession.StoryLoop.Encounters;
using Game.GameMode.StorySession.Utilities;
using Game.GameMode.StorySession.Utilities.Decks;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public class EncounterDeckOrganizer : DeckOrganizer<EncounterType, string>
    {
        public EncounterDeckOrganizer(IRNGManager rngManager) : base(rngManager)
        {
        }
    }
}