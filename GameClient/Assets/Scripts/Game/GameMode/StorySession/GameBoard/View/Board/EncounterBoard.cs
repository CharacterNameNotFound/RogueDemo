using System;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.Board
{
    public class EncounterBoard : MonoBehaviour
    {
        public enum BoardType
        {
            Encounter,
            Battle,
            Inventory
        }
        
        public EncounterBoardView EncounterView;
        public BattleBoardView BattleView;
        public InventoryBoardView InventoryBoardView;

        public void SwitchToView(BoardType boardType)
        {
            EncounterView.Host.gameObject.SetActive(false);
            BattleView.Host.gameObject.SetActive(false);
            InventoryBoardView.Host.gameObject.SetActive(false);

            switch (boardType)
            {
                case BoardType.Encounter:
                    EncounterView.Host.gameObject.SetActive(true);
                    break;
                case BoardType.Battle:
                    BattleView.Host.gameObject.SetActive(true);
                    break;
                case BoardType.Inventory:
                    InventoryBoardView.Host.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(boardType), boardType, null);
            }
        }
        
    }
}