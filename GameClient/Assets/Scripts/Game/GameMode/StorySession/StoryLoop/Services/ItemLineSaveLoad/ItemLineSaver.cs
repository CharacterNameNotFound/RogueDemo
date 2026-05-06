using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemLineSaveLoad
{
    public class ItemLineSaver : IItemLineSaver
    {
        private GameBoardHolder _gameBoardHolder;

        public ItemLineSaver(GameBoardHolder gameBoardHolder)
        {
            _gameBoardHolder = gameBoardHolder;
        }

        public ItemLineSaveData GetSaveData()
        {
            ItemLineComponent playerLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.PlayerItemLine;
            ItemLineComponent stashLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine;

            List<(int index, string itemID)> playerLineSave = ReadItemLine(playerLine);
            List<(int index, string itemID)> stashLineSave = ReadItemLine(stashLine);

            return new ItemLineSaveData(playerLineSave, stashLineSave);
        }

        private List<(int index, string itemID)> ReadItemLine(ItemLineComponent itemLine)
        {
            ItemContainerComponent[] items = itemLine.ItemContainerComponents;

            List<(int index, string itemID)> itemLineSave = new();

            for (int i = 0; i < items.Length;)
            {
                if (items[i] is null)
                {
                    i++;
                    continue;
                }
                
                itemLineSave.Add((i, items[i].StoredItem.ItemId));

                i += items[i].Size;
            }
            
            return itemLineSave;
        }
        
    }
}