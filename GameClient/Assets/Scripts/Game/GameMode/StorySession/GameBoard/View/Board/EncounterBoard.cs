using System;
using System.Collections.Generic;
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

        private Dictionary<BoardType, GameObject> _views;
        
        private void Awake()
        {
            _views = new Dictionary<BoardType, GameObject>()
            {
                { BoardType.Encounter, EncounterView.Host.gameObject},
                { BoardType.Battle, BattleView.Host.gameObject},
                { BoardType.Inventory, InventoryBoardView.Host.gameObject}
            };
        }

        public void HideView(BoardType boardType)
        {
            _views[boardType].SetActive(false);
        }
        
        public void SwitchToView(BoardType boardType)
        {
            foreach (GameObject item in _views.Values)
            {
                item.SetActive(false);
            }
            
            _views[boardType].SetActive(true);
        }
        
    }
}