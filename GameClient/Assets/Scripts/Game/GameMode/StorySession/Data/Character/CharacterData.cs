using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.Data.Character
{
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public string CharacterId { get; private set; }

        [SerializeField] private AssetReferenceSprite _characterPortrait;
        [SerializeField] private AssetReferenceSprite _characterStorySessionPortrait;

        public AssetReferenceDto CharacterPortrait => _characterPortrait.ToAssetReferenceDto();
        public AssetReferenceDto CharacterStorySessionPortrait => _characterStorySessionPortrait.ToAssetReferenceDto();

    }
}