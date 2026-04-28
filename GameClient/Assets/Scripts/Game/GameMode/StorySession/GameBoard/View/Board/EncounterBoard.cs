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
            Stash
        }
        
        public EncounterBoardView EncounterView;
        public BattleBoardView BattleView;
        public StashBoardView StashBoardView;

        public SpriteRenderer SellFirmRenderer;

        private Dictionary<BoardType, GameObject> _views;

        private BoardType? _currentBoard;
        
        private void Awake()
        {
            _views = new Dictionary<BoardType, GameObject>()
            {
                { BoardType.Encounter, EncounterView.Host.gameObject},
                { BoardType.Battle, BattleView.Host.gameObject},
                { BoardType.Stash, StashBoardView.Host.gameObject}
            };
        }

        public BoardType? GetCurrentView()
        {
            return _currentBoard;
        }
        
        public void SwitchToView(BoardType? boardType)
        {
            HideCurrentView();

            if (!boardType.HasValue)
            {
                HideCurrentView();
                return;
            }

            _currentBoard = boardType;
            _views[boardType.Value].SetActive(true);
        }

        public void HideCurrentView()
        {
            if (!_currentBoard.HasValue)
            {
                return;
            }
            
            _views[_currentBoard.Value].SetActive(false);
            _currentBoard = null;

        }

        public void ToggleSellFirm(bool isActive)
        {
            SellFirmRenderer.gameObject.SetActive(isActive);
        }
        
    }
}