using UnityEngine;
using UnityEngine.AddressableAssets;

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

        public void Release()
        {
            Addressables.Release(Portrait.sprite);
        }
    }
}