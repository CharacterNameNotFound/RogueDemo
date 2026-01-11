using Utils.UtilityTypes.AssetReferencing;

namespace Game.Routines.PlayStart.PlayInitialization
{
    public interface IStartingPlayerConfigs
    {
        public AssetReferenceDto ShipReference { get; }
    }
}