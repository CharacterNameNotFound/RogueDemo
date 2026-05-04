using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.GameBoard.Services.BoardModelManipulation
{
    public class BoardModelManipulator : IBoardModelManipulator
    {
        private IGameBoardModelHolder _gameBoardModelHolder;
        private GameBoardHolder _gameBoardHolder;

        
        public BoardModelManipulator(IGameBoardModelHolder gameBoardModelHolder, GameBoardHolder gameBoardHolder)
        {
            _gameBoardModelHolder = gameBoardModelHolder;
            _gameBoardHolder = gameBoardHolder;
        }
        

        public void UpdatePlayerLines()
        {
            UpdateLine(ItemBoardGroup.PlayerMain);
            UpdateLine(ItemBoardGroup.PlayerStash);
        }

        public void UpdateLine(ItemBoardGroup group)
        {
            ItemBoardModel model = GetModelByGroup(group);
            ItemLineComponent view = GetViewByGroup(group);

            for (int i = 0; i < view.ItemContainerComponents.Length; i++)
            {
                if (view.ItemContainerComponents[i] is null)
                {
                    model.Items[i] = null;
                    continue;
                }
                
                model.Items[i] = view.ItemContainerComponents[i].StoredItem;
            }
            
        }

        public void AddItem(Item item, int index, ItemBoardGroup group)
        {
            ItemBoardModel itemBoardModel = GetModelByGroup(group);

            UpdateBoard(item, item, index, itemBoardModel);
        }

        public void Remove(Item item, int index, ItemBoardGroup group)
        {
            ItemBoardModel itemBoardModel = GetModelByGroup(group);

            UpdateBoard(item, null, index, itemBoardModel);
        }


        private void UpdateBoard(Item item, Item newValue, int index, ItemBoardModel itemBoardModel)
        {
            for (int i = 0; i < item.ItemSize; i++)
            {
                itemBoardModel.Items[index + i] = newValue;
            }
        }

        private ItemBoardModel GetModelByGroup(ItemBoardGroup group)
        {
            return group switch
            {
                ItemBoardGroup.PlayerMain => _gameBoardModelHolder.GameBoardModel.PlayerBoard,
                ItemBoardGroup.PlayerStash => _gameBoardModelHolder.GameBoardModel.PlayerStashBoard,
                ItemBoardGroup.Encounter => _gameBoardModelHolder.GameBoardModel.EncounterBoard,
                _ => throw new ArgumentOutOfRangeException(nameof(group), group, null)
            };
        }

        private ItemLineComponent GetViewByGroup(ItemBoardGroup group)
        {
            return group switch
            {
                ItemBoardGroup.PlayerMain => _gameBoardHolder.GameBoardComponent.ItemLineViewController.PlayerItemLine,
                ItemBoardGroup.PlayerStash => _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine,
                ItemBoardGroup.Encounter => _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine,
                _ => throw new ArgumentOutOfRangeException(nameof(group), group, null)
            };
        }

    }
}