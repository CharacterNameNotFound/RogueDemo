using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public class ItemPresenterConfigs : ScriptableObject, IItemPresenterConfigs
    {
        [field: SerializeField] public AssetReferenceSprite BronzeFrame { get; private set; }
        [field: SerializeField] public AssetReferenceSprite SilverFrame { get; private set; }
        [field: SerializeField] public AssetReferenceSprite GoldenFrame { get; private set; }
        [field: SerializeField] public AssetReferenceSprite DiamondFrame { get; private set; }
        [field: SerializeField] public AssetReferenceSprite LegendaryFrame { get; private set; }
    }
}