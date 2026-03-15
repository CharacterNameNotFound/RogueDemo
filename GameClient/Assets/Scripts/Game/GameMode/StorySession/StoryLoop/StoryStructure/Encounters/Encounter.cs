using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.Encounters
{
    public class Encounter : ScriptableObject
    {
        [field: SerializeField] public EncounterType EncounterType { get; set; }
    }
}