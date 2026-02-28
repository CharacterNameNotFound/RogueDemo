using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.Utilities.SceneDataProviding
{
    public class SceneAddressableDataProvider : ScriptableObject, ISceneAddressableDataProvider
    {
        [field: SerializeField] public AssetReference Main { get; private set; }
        [field: SerializeField] public AssetReference StorySession { get; private set; }
        
        public AssetReference MainScene => Main;
        public AssetReference StorySessionScene => StorySession;
    }
}