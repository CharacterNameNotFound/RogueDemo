using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.AudioManager
{
    public interface IAudioManagerConfigurationsProvider
    {
        public AssetReferenceDto SFXAudioPrefab { get; }
        public int SFXPoolSize { get; }
        public AssetReferenceDto MusicAudioPrefab { get; }
    }
}