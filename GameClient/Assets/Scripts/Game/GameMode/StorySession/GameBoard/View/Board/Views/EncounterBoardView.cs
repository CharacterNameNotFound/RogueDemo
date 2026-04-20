using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Views
{
    public class EncounterBoardView : MonoBehaviour
    {
        public Transform Host;
        public SpriteRenderer Portrait;

        public void Render(Sprite portrait)
        {
            Portrait.sprite = portrait;
        }
    }
}