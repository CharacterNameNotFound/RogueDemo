using Utils.UtilityTypes.AssetReferencing;

namespace Game.Utilities.SceneDataProviding
{
    public interface ISceneAddressableDataProvider
    {
        public AssetReferenceDto MainScene { get; }
        public AssetReferenceDto StorySessionScene { get; }
    }
}