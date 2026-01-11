using System;

namespace Utils.UtilityTypes.AssetReferencing
{
    [Serializable]
    public struct AssetReferenceDto
    {
        private object _runtimeKey;

        public object RuntimeKey => _runtimeKey;
        
        public AssetReferenceDto(object assetReferenceRuntimeKey)
        {
            _runtimeKey = assetReferenceRuntimeKey;
        }

    }
}