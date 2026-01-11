using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubGameModeAddressableProvider : ScriptableObject, IScreenAddressableReferenceProvider<MainHubScreenController>
    {
        [field: SerializeField] public PlatformDependentAssetReference ScreenReference { get; private set; }
        
        public AssetReferenceDto GetScreenRuntimeKey(PlatformType platformType)
        {
            return ScreenReference.ToAssetReferenceDto(platformType);
        }
    }
    
}