using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public class EncounterLoaderConfigProvider : ScriptableObject, IEncounterLoaderConfigProvider
    {
        [field: SerializeField] public string EncounterPrefix { get; private set; }
    }
}