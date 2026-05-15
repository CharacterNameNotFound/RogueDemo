using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.Tags;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.ItemRaritySelection;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Utilities;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using UnityEngine;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Gifts
{
    public class TagItemGiftEncounter : GiftEncounter
    {
        [field: Space(20)]
        [field: Header("One Tag Item Gift configs")]
        [field: Tooltip("If checked items containing all tags simultaneously will be selected")]
        [field: SerializeField] public bool OnlyExactTagSequence { get; set; }
        [field: SerializeField] public ItemSelectionGroup IncludedGroups { get; set; }
        [field: SerializeField] public List<ItemTag> FilterTags { get; set; }
        [field: SerializeField] public int ItemCount { get; set; }
        
        private ItemDeckOrganizer _itemDeckOrganizer;
        private IItemRaritySelector _itemRaritySelector;
        private IItemRegistry _itemRegistry;
        private IObtainableItemExclusionListBuilder _obtainableItemExclusionListBuilder;
        
        [Inject]
        private void InjectDependencies(
            ItemDeckOrganizer itemDeckOrganizer,
            IItemRaritySelector itemRaritySelector,
            IItemRegistry itemRegistry,
            IObtainableItemExclusionListBuilder obtainableItemExclusionListBuilder)
        {
            _itemDeckOrganizer = itemDeckOrganizer;
            _itemRaritySelector = itemRaritySelector;
            _itemRegistry = itemRegistry;
            _obtainableItemExclusionListBuilder = obtainableItemExclusionListBuilder;
        }
        
        public override async UniTask<IEnumerable<string>> GetItemList(GameBoardModel gameBoardModel, CancellationToken cancellationToken)
        {
            HashSet<string> excludedItems = await _obtainableItemExclusionListBuilder.BuildIgnoredListIds(gameBoardModel, cancellationToken);
            
            if (IncludedGroups == ItemSelectionGroup.Deck)
            {
                return await GenerateByDeck(gameBoardModel, excludedItems, cancellationToken);
            }
            
            
            throw new NotImplementedException();
        }
        
        private UniTask<HashSet<string>> GenerateByDeck(GameBoardModel gameBoardModel, HashSet<string> excludedItems, CancellationToken cancellationToken)
        {
            List<ItemRarity> itemRarities = _itemRaritySelector.GetRarities(1, gameBoardModel);

            return PullFromCardsDeck(itemRarities, excludedItems, cancellationToken);
        }
        
        private async UniTask<HashSet<string>> PullFromCardsDeck(List<ItemRarity> itemRarities, HashSet<string> excludedItems, CancellationToken cancellationToken)
        {
            HashSet<string> result = new();
            
            foreach (ItemRarity rarity in itemRarities)
            {
                // ToDo: Implement owned filter
                string itemId = DrawOneByRarity(rarity, excludedItems);
                
                await _obtainableItemExclusionListBuilder.AppendItemExclusion(itemId, excludedItems, cancellationToken);
                
                result.Add(itemId);
            }

            return result;
        }
        
        private string DrawOneByRarity(ItemRarity rarity, HashSet<string> excludedItems)
        {
            int sanity = 10000;
            
            for (int i = 0; i < sanity; i++)
            {
                _itemDeckOrganizer.Draw(rarity, false, out string itemId);

                if (excludedItems.Contains(itemId))
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
            
            return DrawOneByRarity((ItemRarity) rarityInt, excludedItems);
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