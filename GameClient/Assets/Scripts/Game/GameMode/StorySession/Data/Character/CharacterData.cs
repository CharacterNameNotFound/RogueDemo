using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.Data.Character
{
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public string CharacterId { get; private set; }

        [SerializeField] private AssetReferenceSprite _characterPortrait;

        public AssetReferenceDto CharacterPortrait => _characterPortrait.ToAssetReferenceDto();

    }
}