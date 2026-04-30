using System.Linq;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public class ItemTransactionOperationController : IItemTransactionOperationController
    {
        private GameBoardHolder _gameBoardHolder;
        private IEncounterPlayer _encounterPlayer;
        private IStoryContextProvider _storyContextProvider;
        private IItemStatGetter _itemStatGetter;

        public ItemTransactionOperationController(
            IEncounterPlayer encounterPlayer, 
            GameBoardHolder gameBoardHolder, 
            IStoryContextProvider storyContextProvider, 
            IItemStatGetter itemStatGetter)
        {
            _encounterPlayer = encounterPlayer;
            _gameBoardHolder = gameBoardHolder;
            _storyContextProvider = storyContextProvider;
            _itemStatGetter = itemStatGetter;
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
            IStoryContext storyContext = _storyContextProvider.StoryContext;

            float price = _itemStatGetter.GetStatValue(item.StoredItem, ItemStatType.Value, StatSet.StatSetComponent.BaseValue, StatSet.StatSetComponent.None);

            return storyContext.GameBoardModel.PlayerStats.Coins >= price;
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