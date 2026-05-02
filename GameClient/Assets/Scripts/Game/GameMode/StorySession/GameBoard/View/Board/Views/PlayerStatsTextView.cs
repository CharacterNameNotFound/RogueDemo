using TMPro;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Views
{
    public class PlayerStatsTextView : MonoBehaviour
    {
        public TMP_Text Text;

        public void SetText(string text)
        {
            Text.text = text;
        }

    }
}