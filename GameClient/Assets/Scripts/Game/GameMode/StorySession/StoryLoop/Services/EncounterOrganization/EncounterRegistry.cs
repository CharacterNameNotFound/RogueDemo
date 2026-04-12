using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.Simulation.Encounters;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public class EncounterRegistry : IEncounterRegistry
    {
        private Dictionary<string, Encounter> _encounters;

        public void Register(List<Encounter> encounters)
        {
            _encounters = encounters.ToDictionary(item => item.EncounterId);
        }

        public bool Append(Encounter encounter)
        {
            return _encounters.TryAdd(encounter.EncounterId, encounter);
        }

        public void AppendRange(IEnumerable<Encounter> encounters)
        {
            foreach (Encounter item in encounters)
            {
                _encounters.TryAdd(item.EncounterId, item);
            }
        }

        public void CleanUp()
        {
            foreach (Encounter item in _encounters.Values.ToArray())
            {
                Addressables.Release(item);
            }
            
        }
        
    }
}