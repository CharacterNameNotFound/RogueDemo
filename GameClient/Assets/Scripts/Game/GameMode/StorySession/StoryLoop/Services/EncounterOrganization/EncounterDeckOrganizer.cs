using Game.GameMode.StorySession.GameBoard.Simulation.Encounters;
using Game.GameMode.StorySession.Utilities;
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