using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.Encounters
{
    public class EncounterGroup : ScriptableObject
    {
        public List<Encounter> Encounters;
    }
}