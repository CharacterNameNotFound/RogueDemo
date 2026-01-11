using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.GameSceneManager.LoadingScreen
{
    public interface IBasicLoadingScreenDataProvider
    {
        public AssetReferenceDto BasicLoadingScreenAddressableKey { get; }

    }
}