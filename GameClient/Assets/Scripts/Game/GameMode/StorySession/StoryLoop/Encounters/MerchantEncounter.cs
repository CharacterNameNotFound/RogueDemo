using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Encounters
{
    public abstract class MerchantEncounter : Encounter
    {
        public enum CountSelectorType
        {
            ItemCount,
            BoardSlotCount
        }

        [field: Space(20)]
        [field: Header("Merchant configs: ")]
        [field: SerializeField] public AssetReferenceSprite MerchantPortrait { get; private set; }
        [field: SerializeField] public CountSelectorType CountSelector { get; private set; }
        [field: SerializeField] public int ItemCount { get; private set; }
        [field: SerializeField] public int BoardSlotCount { get; private set; }

        public override EncounterType EncounterType => EncounterType.Merchant;

        public abstract UniTask<IEnumerable<string>> GetItemList(IStoryContext storyContext, CancellationToken cancellationToken);

    }
}