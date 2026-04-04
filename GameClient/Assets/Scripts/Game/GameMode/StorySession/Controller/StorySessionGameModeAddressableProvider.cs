using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using Game.GameMode.StorySession.UI;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameModeAddressableProvider : ScriptableObject, IScreenAddressableReferenceProvider<StorySessionScreenController>
    {
        [field: SerializeField] public PlatformDependentAssetReference ScreenReference { get; private set; }
        
        public AssetReference GetScreenRuntimeKey(PlatformType platformType)
        {
            return ScreenReference.Get(platformType);
        }
    }
}