using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private List<AssetReference> _itemSets;

        public AssetReferenceSprite CharacterPortrait => _characterPortrait;
        public AssetReferenceSprite CharacterStorySessionPortrait => _characterStorySessionPortrait;
        public List<AssetReference> ItemSets => _itemSets;


    }
}