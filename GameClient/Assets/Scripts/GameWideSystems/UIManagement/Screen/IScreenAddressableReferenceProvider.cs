using Configurations.PlatformDependentFields;
using Utils.UtilityTypes.AssetReferencing;

namespace GameWideSystems.UIManagement.Screen
{
    public interface IScreenAddressableReferenceProvider<T> where T : UIScreenBase
    {
        public AssetReferenceDto GetScreenRuntimeKey(PlatformType platformType);
    }
}