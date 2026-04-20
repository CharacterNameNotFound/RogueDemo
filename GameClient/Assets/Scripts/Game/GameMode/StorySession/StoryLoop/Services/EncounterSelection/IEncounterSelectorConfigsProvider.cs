using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection
{
    public interface IEncounterSelectorConfigsProvider
    {
        public int PreparedSelectionInstancesCount { get; }
        public AssetReferenceGameObject EncounterSelectionPrefab { get; }
        public float SideFreeSpace { get;  }
    }
}