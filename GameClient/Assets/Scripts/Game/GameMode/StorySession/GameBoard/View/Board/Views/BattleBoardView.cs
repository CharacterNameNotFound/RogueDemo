using Game.GameMode.StorySession.GameBoard.View.Board.Elements;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Views
{
    public class BattleBoardView : MonoBehaviour
    {
        public Transform Host;
        public SpriteRenderer Portrait;
        public HpBarComponent HpBar;

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