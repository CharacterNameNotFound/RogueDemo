using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.Data.Items
{
    public class EncounterSet : ScriptableObject
    {
        [field: SerializeField] public List<AssetReference> Encounters { get; private set; }
    }
}