using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using Game.GameMode.StorySession.UI;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameModeAddressableProvider : ScriptableObject, IScreenAddressableReferenceProvider<StorySessionScreenController>
    {
        [field: SerializeField] public PlatformDependentAssetReference ScreenReference { get; private set; }
        
        public AssetReferenceDto GetScreenRuntimeKey(PlatformType platformType)
        {
            return ScreenReference.ToAssetReferenceDto(platformType);
        }
    }
}