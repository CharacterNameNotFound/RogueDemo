using System;
using GameWideSystems.LocalizationWrapper;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Encounters
{
    public abstract class Encounter : ScriptableObject
    {
        
        [field: Header("Encounter configs")]
        [field: SerializeField] public string EncounterId { get; protected set; }
        [field: SerializeField] public LocalizedLineKey Name { get; protected set; }
        [field: SerializeField] public LocalizedLineKey Description { get; protected set; }
        [field: SerializeField] public AssetReferenceSprite Portrait { get; protected set; }

        [field: Space(10)]
        [field: SerializeField] public bool IsRepeatable { get; private set; } = true;

        public abstract EncounterType EncounterType { get; }
        
        public virtual string[] GetPreloadedItemIds()
        {
            return Array.Empty<string>();
        }
        
        
        
        
    }
}