using UnityEngine;
using UnityEngine.Serialization;

namespace Game.GameMode.StorySession.GameBoard.View.Board
{
    public class GameBoardComponent : MonoBehaviour
    {
        [field: SerializeField] public EncounterBoard EncounterBoard { get; private set; }
        [field: SerializeField] public PlayerBoard PlayerBoard { get; private set; }
        [field: SerializeField] public ItemLineViewController ItemLineViewController { get; private set; }
    }
}