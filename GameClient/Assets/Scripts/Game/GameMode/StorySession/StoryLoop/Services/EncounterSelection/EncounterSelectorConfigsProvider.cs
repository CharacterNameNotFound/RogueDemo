using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection
{
    public class EncounterSelectorConfigsProvider : ScriptableObject, IEncounterSelectorConfigsProvider
    {
        [field: SerializeField] public int PreparedSelectionInstancesCount { get; private set; } = 6;
        [field: SerializeField] public AssetReferenceGameObject EncounterSelectionPrefab { get; private set; }
        [field: SerializeField] public float SideFreeSpace { get; private set; }
    }
}