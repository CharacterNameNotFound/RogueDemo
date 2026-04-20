using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.Tags;
using Game.GameMode.StorySession.StoryLoop.Encounters.Merchants.ItemRaritySelection;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using Game.ManagementSystems.LookUpTableManagement;
using GameWideSystems.RNGManagement;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Game.GameMode.StorySession.StoryLoop.Encounters.Merchants
{
    public class TagSelectorMerchant : MerchantEncounter
    {
        [field: Space(20)]
        [field: Header("Tag Selector Merchant configs")]
        [field: Tooltip("If checked items containing all tags simultaneously will be selected")]
        [field: SerializeField] public bool OnlyExactTagSequence { get; set; }
        [field: SerializeField] public ItemSelectionGroup IncludedGroups { get; set; }
        [field: SerializeField] public List<ItemTag> FilterTags { get; set; }

        private ILookUpTableManager _lookUpTableManager;
        private ItemDeckOrganizer _itemDeckOrganizer;
        private IItemRaritySelector _itemRaritySelector;
        private IRNGManager _rngManager;
        private IItemRegistry _itemRegistry;
        
        [Inject]
        private void InjectDependencies(
            ILookUpTableManager lookUpTableManager,
            ItemDeckOrganizer itemDeckOrganizer,
            IItemRaritySelector itemRaritySelector,
            IRNGManager rngManager,
            IItemRegistry itemRegistry)
        {
            _lookUpTableManager = lookUpTableManager;
            _itemDeckOrganizer = itemDeckOrganizer;
            _itemRaritySelector = itemRaritySelector;
            _rngManager = rngManager;
            _itemRegistry = itemRegistry;
        }

        public override async UniTask<List<string>> GetItemList(IStoryContext storyContext, CancellationToken cancellationToken)
        {
            if (IncludedGroups == ItemSelectionGroup.Deck)
            {
                return GenerateByDeck(storyContext, cancellationToken);
            }
            
            
            throw new NotImplementedException();
        }

        private List<string> GenerateByDeck(IStoryContext storyContext, CancellationToken cancellationToken)
        {
            List<ItemRarity> itemRarities = _itemRaritySelector.GetRarities(ItemCount, storyContext);

            return PullFromCardsDeck(itemRarities);
        }

        private List<string> PullFromCardsDeck(List<ItemRarity> itemRarities)
        {
            List<string> result = new();
            
            foreach (ItemRarity rarity in itemRarities)
            {
                // ToDo: Implement owned filter
                string itemId = DrawOneByRarity(rarity, result, new List<string>());
                
                result.Add(itemId);
            }

            return result;
        }

        private string DrawOneByRarity(ItemRarity rarity, List<string> drawnList, List<string> ownedList)
        {
            int sanity = 10000;
            
            for (int i = 0; i < sanity; i++)
            {
                _itemDeckOrganizer.Draw(rarity, false, out string itemId);

                if (drawnList.Contains(itemId) || ownedList.Contains(itemId))
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
            
            
            throw new InvalidOperationException("Failed to draw an item within a sanity limit");
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