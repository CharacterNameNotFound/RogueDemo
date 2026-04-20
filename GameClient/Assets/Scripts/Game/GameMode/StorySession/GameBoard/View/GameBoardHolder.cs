using Game.GameMode.StorySession.GameBoard.View.Board;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.View
{
    public class GameBoardHolder
    {
        public Transform GameHolder;
        public Transform VFXHolder;
        
        public GameBoardComponent GameBoardComponent;

        public void Initialize()
        {
            GameHolder = new GameObject("Game_holder").transform;
            VFXHolder = new GameObject("VFX_holder").transform;
        }

        public void Cleanup()
        {
            GameHolder = null;
            VFXHolder = null;

            Addressables.ReleaseInstance(GameBoardComponent.gameObject);
        }
        
    }
}