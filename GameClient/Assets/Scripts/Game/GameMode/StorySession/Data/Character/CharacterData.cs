using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.Data.Character
{
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public string CharacterId { get; private set; }
        [field: SerializeField] public int Index { get; private set; }

        [field: SerializeField] public AssetReferenceSprite CharacterPortrait { get; private set; }
        [field: SerializeField] public AssetReferenceSprite CharacterStorySessionPortrait { get; private set; }
        [field: SerializeField] public List<AssetReference> ItemSets { get; private set; }
        [field: SerializeField] public List<AssetReference> EncounterSets { get; private set; }
        

    }
}