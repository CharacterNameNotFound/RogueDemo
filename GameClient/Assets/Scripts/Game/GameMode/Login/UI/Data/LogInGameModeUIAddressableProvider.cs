using Configurations.PlatformDependentFields;
using Configurations.PlatformDependentFields.Implementations;
using Game.GameMode.Login.UI.Screens;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;


namespace Game.GameMode.Login.UI.Data
{
    public class LogInGameModeUIAddressableProvider : ScriptableObject, IScreenAddressableReferenceProvider<LogInScreenController>
    {
        [field: SerializeField] public PlatformDependentAssetReference ScreenReference { get; private set; }
        
        public AssetReference GetScreenRuntimeKey(PlatformType platformType)
        {
            return ScreenReference.Get(platformType);
        }
        
    }
}