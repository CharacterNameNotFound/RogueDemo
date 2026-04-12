using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Encounters;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public interface IEncounterRegistry
    {
        public void Register(List<Encounter> encounters);
        public bool Append(Encounter encounter);
        public void AppendRange(IEnumerable<Encounter> encounters);
        public void CleanUp();

    }
}