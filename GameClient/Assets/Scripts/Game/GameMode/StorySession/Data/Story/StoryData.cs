using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.Data.Story
{
    public class StoryData : ScriptableObject
    {
        [field: SerializeField] public int SortingOrder { get; private set; }
        [field: SerializeField] public string StoryId { get; private set; }

        [SerializeField] private AssetReferenceSprite _storyImage;

        public AssetReference StoryImage => _storyImage;
        
    }
}