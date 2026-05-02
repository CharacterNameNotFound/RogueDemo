using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.Utilities.WorldInteractebles;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.Board
{
    public class GameBoardInteractables : MonoBehaviour
    {
        [Header("World buttons")]
        public WorldButton EventEncounterScreenButton;
        public WorldButton BattleScreenButton;
        public WorldButton InventoryButton;

        [Header("Texts")] 
        public GameCyclesTextView GameCyclesTextView;
        public PlayerStatsTextView PlayerStatsTextView;

    }
}