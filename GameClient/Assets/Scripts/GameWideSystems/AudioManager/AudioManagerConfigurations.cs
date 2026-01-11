using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.AudioManager
{
    public class AudioManagerConfigurations : ScriptableObject, IAudioManagerConfigurationsProvider
    {
        [SerializeField] private AssetReference _sfxAudioPrefabReference;
        [SerializeField] private AssetReference _musicAudioPrefabReference;
        [field: SerializeField] public int SFXPoolSize { get; private set; }

        public AssetReferenceDto SFXAudioPrefab => _sfxAudioPrefabReference.ToAssetReferenceDto();
        public AssetReferenceDto MusicAudioPrefab  => _sfxAudioPrefabReference.ToAssetReferenceDto();
        
    }
}