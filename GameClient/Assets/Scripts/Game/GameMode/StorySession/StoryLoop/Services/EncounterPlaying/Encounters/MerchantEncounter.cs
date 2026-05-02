using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating;
using Game.GameMode.StorySession.GameBoard.Services.TextsDrawing;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.Utilities.EventArguments;
using Game.GameMode.StorySession.Utilities.WorldInteractables.Awaiters;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.EventProcessing;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters
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
        [field: SerializeField] public int PriceMultiplier { get; private set; } = 2;

        private GameBoardHolder _gameBoardHolder;
        private IMerchantEncounterRoutines _merchantEncounterRoutines;
        private IStoryContextProvider _storyContextProvider;
        private IItemStatGetter _itemStatGetter;
        private IPlayerStatusUpdater _statusUpdater;
        
        private IEventDispatcher<PreItemPurchaseArguments> _preItemPurchase;
        
        public override EncounterType EncounterType => EncounterType.Merchant;
        
        public abstract UniTask<IEnumerable<string>> GetItemList(IStoryContext storyContext, CancellationToken cancellationToken);

        [Inject]
        private void Construct(
            GameBoardHolder gameBoardHolder, 
            IMerchantEncounterRoutines merchantEncounterRoutines, 
            IStoryContextProvider storyContextProvider,
            IItemStatGetter itemStatGetter,
            IEventDispatcher<PreItemPurchaseArguments> preItemPurchase,
            IPlayerStatusUpdater statusUpdater)
        {
            _gameBoardHolder = gameBoardHolder;
            _merchantEncounterRoutines = merchantEncounterRoutines;
            _storyContextProvider = storyContextProvider;
            _itemStatGetter = itemStatGetter;
            _preItemPurchase = preItemPurchase;
            _statusUpdater = statusUpdater;
        }
        
        
        public override async UniTask Play(IStoryContext storyContext, CancellationToken cancellationToken)
        {
            await _merchantEncounterRoutines.ShowElements(this, storyContext, cancellationToken);

            IEnumerable<string> items = await GetItemList(storyContext, cancellationToken);
            await _merchantEncounterRoutines.ShowWares(items, storyContext, cancellationToken);
            UpdatePriceTags();
            
            _preItemPurchase.RegisterHandler(OnPrePurchase);
            
            await InteractablePressWaiter.WaitForPress(
                _gameBoardHolder.GameBoardComponent.GameBoardInteractables.EventEncounterScreenButton, 
                cancellationToken);
            
            _preItemPurchase.Unregister(OnPrePurchase);

            await _merchantEncounterRoutines.HideAll(cancellationToken);

        }

        public override bool CanPurchase(Item item)
        {
            IStoryContext storyContext = _storyContextProvider.StoryContext;

            float value = _itemStatGetter.GetStatValue(item, ItemStatType.Value, StatSet.StatSetComponent.BaseValue, StatSet.StatSetComponent.None);
            
            return storyContext.GameBoardModel.PlayerStats.Coins >= ValueToPrice(value);
        }

        private void UpdatePriceTags()
        {
            ItemContainerComponent[] itemContainerComponents = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine.ItemContainerComponents;
            
            int i = Array.FindIndex(itemContainerComponents, x => x is not null);

            for (; i < itemContainerComponents.Length;)
            {
                float value = _itemStatGetter.GetStatValue(
                    itemContainerComponents[i].StoredItem, 
                    ItemStatType.Value,
                    StatSet.StatSetComponent.Special, 
                    StatSet.StatSetComponent.Special);
                
                itemContainerComponents[i].SetPriceTag(ValueToPrice(value));

                i += itemContainerComponents[i].Size;
                
                if (itemContainerComponents[i] is null)
                {
                    break;
                }
            }
        }
        

        private UniTask OnPrePurchase(PreItemPurchaseArguments preItemPurchaseArguments, CancellationToken cancellationToken)
        {
            float value = _itemStatGetter.GetStatValue(
                preItemPurchaseArguments.ItemContainerComponent.StoredItem, 
                ItemStatType.Value,
                StatSet.StatSetComponent.Special, 
                StatSet.StatSetComponent.Special);
            
            _statusUpdater.UpdateCoins(-ValueToPrice(value));

            preItemPurchaseArguments.ItemContainerComponent.SetPriceTag(value);
            
            return UniTask.CompletedTask;
        }

        private int ValueToPrice(float value)
        {
            return Mathf.CeilToInt(value) * PriceMultiplier;
        }
        
    }
}