using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.Encounters
{
    public abstract class Encounter : ScriptableObject
    {
        public abstract EncounterType EncounterType { get; }
        
        [field: SerializeField] public AssetReferenceSprite Portrait;

        public virtual string[] GetPreloadedItemIds()
        {
            return Array.Empty<string>();
        }
        

    }
}