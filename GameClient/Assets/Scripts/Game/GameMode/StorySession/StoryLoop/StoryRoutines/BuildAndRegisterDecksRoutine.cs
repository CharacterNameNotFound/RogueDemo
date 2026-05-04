using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Items;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines.DataProviders;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using Game.GameMode.StorySession.Utilities;
using Game.GameMode.StorySession.Utilities.Decks;
using GameWideSystems.RNGManagement;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.StoryLoop.StoryRoutines
{
    public class BuildAndRegisterDecksRoutine
    {
        
        private ItemDeckOrganizer _itemDeckOrganizer;
        private IItemRegistry _itemRegistry;

        private EncounterDeckOrganizer _encounterDeckOrganizer;
        private IEncounterRegistry _encounterRegistry;

        private IRNGManager _rngManager;
        private JsonSerializerSettings _jsonSerializerSettings;

        public BuildAndRegisterDecksRoutine(
            ItemDeckOrganizer itemDeckOrganizer, 
            IItemRegistry itemRegistry, 
            JsonSerializerSettings jsonSerializerSettings, 
            EncounterDeckOrganizer encounterDeckOrganizer, 
            IEncounterRegistry encounterRegistry, 
            IRNGManager rngManager
            )
        {
            _itemDeckOrganizer = itemDeckOrganizer;
            _itemRegistry = itemRegistry;
            _jsonSerializerSettings = jsonSerializerSettings;
            _encounterDeckOrganizer = encounterDeckOrganizer;
            _encounterRegistry = encounterRegistry;
            _rngManager = rngManager;
        }

        public async UniTask BuildAndRegistriesItems(
            List<AssetReference> neutralItemSetsReferences,
            List<AssetReference> characterItemSetsReferences,
            IItemDeckBuildingConfigs itemDeckBuildingConfigs,
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
                
                for (int i = 0; i < itemDeckBuildingConfigs.CardCopiesCount; i++)
                {
                    deck.Add(item.ItemId);
                }
                
            }

            IRNGProvider randomProvider = _rngManager.GetRandomProvider(RNGGroup.CardShuffler);
            Dictionary<ItemRarity, IDeck<string>> itemDecks = decks.ToDictionary(
                    item => item.Key, 
                    item => (IDeck<string>) new ItemDeck(item.Value, randomProvider)
                    );

            _itemDeckOrganizer.Initialize(itemDecks);
            _itemDeckOrganizer.ShuffleAll();
        }

        public async UniTask BuildAndRegisterEncounters(
            List<AssetReference> characterEncounterSets, 
            List<AssetReference> storyEncounterSet, 
            IEncounterDeckBuildingConfigs encounterDeckBuildingConfigs, 
            CancellationToken cancellationToken)
        {
            List<UniTask<EncounterSet>> encounterSetTasks = new();
            List<UniTask<Encounter>> encounterTasks = new();

            AppendItemSetLoading(encounterSetTasks, characterEncounterSets, cancellationToken);
            AppendItemSetLoading(encounterSetTasks, storyEncounterSet, cancellationToken);

            // Loading encounter sets
            EncounterSet[] itemSets = await UniTask.WhenAll(encounterSetTasks).AttachExternalCancellation(cancellationToken);

            // Selecting and loading encounters
            foreach (AssetReference item in itemSets.SelectMany(item => item.Encounters))
            {
                encounterTasks.Add(item.Load<Encounter>(cancellationToken));
            }

            Encounter[] encounters = await UniTask.WhenAll(encounterTasks).AttachExternalCancellation(cancellationToken);
            
            _encounterRegistry.Initialize(encounters.ToList());
            
            // we no longer need encounter sets, so we can release them
            foreach (EncounterSet item in itemSets)
            {
                Addressables.Release(item);
            }

            EncounterType[] encounterTypes = (EncounterType[]) Enum.GetValues(typeof(EncounterType));
            Dictionary<EncounterType, List<string>> decks = new Dictionary<EncounterType, List<string>>();

            foreach (EncounterType item in encounterTypes)
            {
                decks.Add(item, new List<string>());
            }
            
            foreach (Encounter item in encounters)
            {
                List<string> deck = decks[item.EncounterType];
                
                deck.Add(item.EncounterId);
            }

            IRNGProvider randomProvider = _rngManager.GetRandomProvider(RNGGroup.CardShuffler);
            Dictionary<EncounterType, IDeck<string>> encounterDeck = decks.ToDictionary(
                    item => item.Key, 
                    item => (IDeck<string>) new EncounterDeck(item.Value, randomProvider)
                    );

            _encounterDeckOrganizer.Initialize(encounterDeck);
            _encounterDeckOrganizer.ShuffleAll();
        }
        

        private void AppendItemSetLoading<T>(List<UniTask<T>> itemSetsTasks, List<AssetReference> itemSetsReferences, CancellationToken cancellationToken)
        {
            foreach (AssetReference reference in itemSetsReferences)
            {
                UniTask<T> item  = reference.Load<T>(cancellationToken);
                itemSetsTasks.Add(item);
            }
        }
        
    }
}