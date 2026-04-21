using UnityEngine;
using UnityEngine.Serialization;

namespace Game.GameMode.StorySession.GameBoard.View.Board
{
    public class GameBoardComponent : MonoBehaviour
    {
        public EncounterBoard EncounterBoard;
        public PlayerBoard PlayerBoard;
        public ItemLineViewController ItemLineViewController;
        public GameBoardInteractables GameBoardInteractables;
    }
}