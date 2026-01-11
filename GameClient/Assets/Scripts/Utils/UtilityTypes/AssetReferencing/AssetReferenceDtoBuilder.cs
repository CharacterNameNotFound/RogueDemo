using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using UnityEngine.AddressableAssets;

namespace Utils.UtilityTypes.AssetReferencing
{
    public static class AssetReferenceDtoBuilder
    {
        public static AssetReferenceDto ToAssetReferenceDto(this AssetReference assetReference)
        {
            return new AssetReferenceDto(assetReference.RuntimeKey);
        }

        public static AssetReferenceDto ToAssetReferenceDto(this PlatformDependentAssetReference platformDependentAssetReference, PlatformType platformType)
        {
            return new AssetReferenceDto(platformDependentAssetReference.Get(platformType).RuntimeKey);
        }

        public static AssetReferenceDto ToAssetReferenceDto(this object objectKey)
        {
            return new AssetReferenceDto(objectKey);
        }

        
    }
}