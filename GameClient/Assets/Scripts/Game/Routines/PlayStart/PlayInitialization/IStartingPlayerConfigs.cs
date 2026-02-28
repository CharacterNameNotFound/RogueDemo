using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.Routines.PlayStart.PlayInitialization
{
    public interface IStartingPlayerConfigs
    {
        public AssetReference ShipReference { get; }
    }
}