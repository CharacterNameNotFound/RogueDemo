using Game.GameMode.StorySession.StoryLoop.Encounters;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection
{
    public class EncounterSelectorEntryComponent : MonoBehaviour
    {
        public Encounter Encounter;
        public SpriteRenderer SpriteRenderer;
        public int ItemId = 0;
        public bool IsSelected = false;
    }
    
}