using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using Game.GameMode.StorySelector.UI;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySelector.Controller
{
    public class StorySelectorGameModeAddressableProvider : ScriptableObject, IScreenAddressableReferenceProvider<StorySelectorScreenController>
    {
        [field: SerializeField] public PlatformDependentAssetReference ScreenReference { get; private set; }
        
        public AssetReference GetScreenRuntimeKey(PlatformType platformType)
        {
            return ScreenReference.Get(platformType);
        }
    }
}