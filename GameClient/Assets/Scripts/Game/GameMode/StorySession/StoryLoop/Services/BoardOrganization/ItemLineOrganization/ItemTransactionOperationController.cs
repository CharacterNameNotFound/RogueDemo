using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public class ItemTransactionOperationController : IItemTransactionOperationController
    {
        private GameBoardHolder _gameBoardHolder;
        private IEncounterPlayer _encounterPlayer;
        private IItemTransactionEventPublisher _transactionEventPublisher;
        private IItemLineOrganizer _itemLineOrganizer;
        private IItemRemover _itemRemover;
        private IItemStatGetter _itemStatGetter;
        private IPlayerStatusUpdater _statusUpdater;



        public ItemTransactionOperationController(
            IEncounterPlayer encounterPlayer, 
            GameBoardHolder gameBoardHolder, 
            IItemTransactionEventPublisher transactionEventPublisher, 
            IItemLineOrganizer itemLineOrganizer, 
            IItemRemover itemRemover, 
            IItemStatGetter itemStatGetter, 
            IPlayerStatusUpdater statusUpdater)
        {
            _encounterPlayer = encounterPlayer;
            _gameBoardHolder = gameBoardHolder;
            _transactionEventPublisher = transactionEventPublisher;
            _itemLineOrganizer = itemLineOrganizer;
            _itemRemover = itemRemover;
            _itemStatGetter = itemStatGetter;
            _statusUpdater = statusUpdater;
        }

        public bool IsPurchaseAttempt(ItemContainerComponent item, ItemLineComponent originalItemLine)
        {
            if (_encounterPlayer.CurrentEncounter is null)
            {
                return false;
            }
            
            if (_encounterPlayer.CurrentEncounter.EncounterType != EncounterType.Merchant)
            {
                return false;
            }

            if (originalItemLine != _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine)
            {
                return false;
            }
            
            return true;
        }

        public bool CanMoveItem(ItemContainerComponent item, ItemLineComponent originalItemLine)
        {
            return _encounterPlayer.CanMoveItem(item, originalItemLine);
        }

        public bool CanSellItem(ItemContainerComponent item, ItemLineComponent originalItemLine)
        {
            if (originalItemLine != _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine &&
                originalItemLine != _gameBoardHolder.GameBoardComponent.ItemLineViewController.PlayerItemLine)
            {
                return false;
            }
            
            return _encounterPlayer.CanSellItem(item);
        }
        
        public bool CanUpgrade(
            ItemContainerComponent item, 
            ItemLineComponent originalItemLine, 
            out ItemLineComponent upgradableLine, 
            out ItemContainerComponent upgradableItem)
        {
            ItemLineComponent inventoryItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine;
            ItemLineComponent playerItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.PlayerItemLine;

            upgradableLine = null;
            upgradableItem = null;
            
            if (originalItemLine == inventoryItemLine ||
                originalItemLine == playerItemLine)
            {
                return false;
            }

            if (ContainsUpdatableItemInLine(playerItemLine, item, out upgradableItem))
            {
                upgradableLine = playerItemLine;
                return true;
            }
            
            if (ContainsUpdatableItemInLine(inventoryItemLine, item, out upgradableItem))
            {
                upgradableLine = inventoryItemLine;
                return true;
            }
            
            return false;
        }

        public bool CanPurchase(ItemContainerComponent item, ItemLineComponent originalItemLine)
        {
            if (_encounterPlayer.CurrentEncounter is null)
            {
                return false;
            }
            
            return _encounterPlayer.CurrentEncounter.CanPurchase(item.StoredItem);
        }

        public async UniTask SellItem(ItemContainerComponent item, ItemContainerComponent[] itemLineComponent, CancellationToken cancellationToken)
        {
            await _transactionEventPublisher.HandlePreItemSell(item, cancellationToken);
            
            float statValue = _itemStatGetter.GetStatValue(item.StoredItem, ItemStatType.Value, StatSet.StatSetComponent.Special, StatSet.StatSetComponent.Special);
            _statusUpdater.UpdateCoins(statValue);
            
            _itemLineOrganizer.RemoveItem(itemLineComponent, item, out _);
            Item itemStoredItem = item.StoredItem;
            _itemRemover.RemoveItem(item);


            await _transactionEventPublisher.HandlePostItemSell(itemStoredItem, cancellationToken);
        }

        private bool ContainsUpdatableItemInLine(
            ItemLineComponent itemLine, 
            ItemContainerComponent item,
            out ItemContainerComponent upgradableItem)
        {
            for (int i = 0; i < itemLine.ItemContainerComponents.Length; i++)
            {
                ItemContainerComponent tempItem = itemLine.ItemContainerComponents[i];

                if (tempItem is null ||
                    tempItem == item ||
                    tempItem.StoredItem.ItemId != item.StoredItem.ItemId)
                {
                    continue;
                }

                upgradableItem = tempItem;
                return true;
            }

            upgradableItem = null;
            return false;
        }
    }
}