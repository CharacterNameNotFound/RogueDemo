using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.Tags;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.ItemRaritySelection;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.Utilities;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using UnityEngine;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants
{
    public class TagSelectorMerchant : MerchantEncounter
    {
        [field: Space(20)]
        [field: Header("Tag Selector Merchant configs")]
        [field: Tooltip("If checked items containing all tags simultaneously will be selected")]
        [field: SerializeField] public bool OnlyExactTagSequence { get; set; }
        [field: SerializeField] public ItemSelectionGroup IncludedGroups { get; set; }
        [field: SerializeField] public List<ItemTag> FilterTags { get; set; }

        private ItemDeckOrganizer _itemDeckOrganizer;
        private IItemRaritySelector _itemRaritySelector;
        private IItemRegistry _itemRegistry;
        private IMerchantItemExclusionListBuilder _merchantItemExclusionListBuilder;
        
        [Inject]
        private void InjectDependencies(
            ItemDeckOrganizer itemDeckOrganizer,
            IItemRaritySelector itemRaritySelector,
            IItemRegistry itemRegistry,
            IMerchantItemExclusionListBuilder merchantItemExclusionListBuilder)
        {
            _itemDeckOrganizer = itemDeckOrganizer;
            _itemRaritySelector = itemRaritySelector;
            _itemRegistry = itemRegistry;
            _merchantItemExclusionListBuilder = merchantItemExclusionListBuilder;
        }

        public override async UniTask<IEnumerable<string>> GetItemList(GameBoardModel gameBoardModel, CancellationToken cancellationToken)
        {
            HashSet<string> excludedItems = await _merchantItemExclusionListBuilder.BuildIgnoredListIds(gameBoardModel, cancellationToken);
            
            if (IncludedGroups == ItemSelectionGroup.Deck)
            {
                return GenerateByDeck(gameBoardModel, excludedItems, cancellationToken);
            }
            
            
            throw new NotImplementedException();
        }

        private HashSet<string> GenerateByDeck(GameBoardModel gameBoardModel, HashSet<string> excludedItems, CancellationToken cancellationToken)
        {
            List<ItemRarity> itemRarities = _itemRaritySelector.GetRarities(ItemCount, gameBoardModel);

            return PullFromCardsDeck(itemRarities, excludedItems);
        }

        private HashSet<string> PullFromCardsDeck(List<ItemRarity> itemRarities, HashSet<string> excludedItems)
        {
            HashSet<string> result = new();
            
            foreach (ItemRarity rarity in itemRarities)
            {
                // ToDo: Implement owned filter
                string itemId = DrawOneByRarity(rarity, result, excludedItems);
                
                result.Add(itemId);
            }

            return result;
        }

        private string DrawOneByRarity(ItemRarity rarity, HashSet<string> drawnList, HashSet<string> excludedItems)
        {
            int sanity = 10000;
            
            for (int i = 0; i < sanity; i++)
            {
                _itemDeckOrganizer.Draw(rarity, false, out string itemId);

                if (drawnList.Contains(itemId) || excludedItems.Contains(itemId))
                {
                    _itemDeckOrganizer.Return(rarity, itemId);
                    continue;
                }
                
                //items in deck are present in registry
                _itemRegistry.TryGetById(itemId, out Item itemInstance);

                if (IsCorrectTagLine(itemInstance))
                {
                    return itemId;
                }
                
                _itemDeckOrganizer.Return(rarity, itemId);
            }

            
            if (rarity is ItemRarity.Legendary or ItemRarity.Diamond)
            {
                throw new InvalidOperationException("Failed to draw an item within a sanity limit and all possible upgrades");
            }

            int rarityInt = (int)rarity + 1;
            
            return DrawOneByRarity((ItemRarity) rarityInt, drawnList, excludedItems);
        }
        

        private bool IsCorrectTagLine(Item item)
        {
            if (!OnlyExactTagSequence)
            {
                return FilterTags.Any(tag => item.Tags.ContainsTag(tag));
            }

            return FilterTags.All(tag => item.Tags.ContainsTag(tag));
        }

    }
}