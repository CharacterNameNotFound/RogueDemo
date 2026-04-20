using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public interface IItemPresenterConfigs
    {
        public AssetReferenceSprite BronzeFrame { get; }
        public AssetReferenceSprite SilverFrame { get; }
        public AssetReferenceSprite GoldenFrame { get; }
        public AssetReferenceSprite DiamondFrame { get; }
        public AssetReferenceSprite LegendaryFrame { get; }
    }
}