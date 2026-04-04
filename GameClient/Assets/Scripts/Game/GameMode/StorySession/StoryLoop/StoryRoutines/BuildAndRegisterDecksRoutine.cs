using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines.DataProviders;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.StoryLoop.StoryRoutines
{
    public class BuildAndRegisterDecksRoutine
    {
        private IDeckOrganizer _deckOrganizer;
        private IItemRegistry _itemRegistry;
        private JsonSerializerSettings _jsonSerializerSettings;

        public BuildAndRegisterDecksRoutine(
            IDeckOrganizer deckOrganizer, 
            IItemRegistry itemRegistry, 
            JsonSerializerSettings jsonSerializerSettings)
        {
            _deckOrganizer = deckOrganizer;
            _itemRegistry = itemRegistry;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public async UniTask BuildBasicItemsAndRegistries(List<AssetReference> neutralItemSetsReferences,
            List<AssetReference> characterItemSetsReferences,
            IDeckBuildingConfigs deckBuildingConfigs,
            IStoryContentProvider storyContentProvider,
            CancellationToken cancellationToken)
        {
            List<UniTask<ItemSet>> itemSetsTasks = new();

            AppendItemSetLoading(itemSetsTasks, neutralItemSetsReferences, cancellationToken);
            AppendItemSetLoading(itemSetsTasks, characterItemSetsReferences, cancellationToken);
            AppendItemSetLoading(itemSetsTasks, storyContentProvider.NeutralItemSets, cancellationToken);

            ItemSet[] itemSets = await UniTask.WhenAll(itemSetsTasks).AttachExternalCancellation(cancellationToken);
            
            List<Item> items = new List<Item>();
            
            foreach (ItemSet itemSet in itemSets)
            {
                foreach (TextAsset itemTextAsset in itemSet.TextAssets)
                {
                    Item item = JsonConvert.DeserializeObject<Item>(itemTextAsset.text, _jsonSerializerSettings);
                
                    items.Add(item);
                }
            }

            foreach (ItemSet itemSet in itemSets)
            {
                Addressables.Release(itemSet);
            }
            
            _itemRegistry.Initialize(items.ToDictionary(item => item.ItemId));

            ItemRarity[] rarities = (ItemRarity[]) Enum.GetValues(typeof(ItemRarity));
            Dictionary<ItemRarity, List<string>> decks = new Dictionary<ItemRarity, List<string>>();

            foreach (ItemRarity item in rarities)
            {
                decks.Add(item, new List<string>());
            }
            
            foreach (Item item in items)
            {
                List<string> deck = decks[item.ItemRarity];
                
                for (int i = 0; i < deckBuildingConfigs.CardCopiesCount; i++)
                {
                    deck.Add(item.ItemId);
                }
                
            }

            Dictionary<ItemRarity, ItemDeck> itemDecks = decks.ToDictionary(
                    item => item.Key, 
                    item => new ItemDeck(item.Value)
                    );

            _deckOrganizer.Initialize(itemDecks);
        }

        private void AppendItemSetLoading(List<UniTask<ItemSet>> itemSetsTasks, List<AssetReference> itemSetsReferences, CancellationToken cancellationToken)
        {
            foreach (AssetReference reference in itemSetsReferences)
            {
                UniTask<ItemSet> item  = reference.Load<ItemSet>(cancellationToken);
                itemSetsTasks.Add(item);
            }
        }
        
    }
}