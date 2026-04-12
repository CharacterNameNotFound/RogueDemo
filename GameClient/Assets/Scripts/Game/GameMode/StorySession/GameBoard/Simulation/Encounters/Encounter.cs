using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.Encounters
{
    public abstract class Encounter : ScriptableObject
    {
        [field: SerializeField] public string Name { get; protected set; }
        [field: SerializeField] public string Description { get; protected set; }
        [field: SerializeField] public AssetReferenceSprite Portrait { get; protected set; }

        public abstract EncounterType EncounterType { get; }
        
        public virtual string[] GetPreloadedItemIds()
        {
            return Array.Empty<string>();
        }
        
        

    }
}