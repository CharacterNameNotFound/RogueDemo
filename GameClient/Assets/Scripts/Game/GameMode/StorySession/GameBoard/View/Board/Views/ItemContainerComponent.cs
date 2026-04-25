using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using UnityEngine;
using UnityEngine.Rendering;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Views
{
    public class ItemContainerComponent : PoolableGameObject
    {
        [field: SerializeField] public int Size { get; private set; }
        [field: SerializeField] public SpriteRenderer ItemRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer FrameRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer ChargeProgressRenderer { get; private set; }
        [field: SerializeField] public SortingGroup SortingGroup { get; private set; }

        public Item StoredItem;

        public override void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            
        }

        public Vector3 GetItemLinePivot()
        {
            Bounds bounds = ItemRenderer.bounds;

            float pivotX = bounds.min.x + bounds.size.x / Size / 2f;

            return new Vector3(pivotX, bounds.center.y, 0);
        }
        
        public void RenderStarMovement()
        {
            SortingGroup.sortingOrder = 1;
        }
        
        public void RenderEndMovement()
        {
            SortingGroup.sortingOrder = 0;
        }
        
    }
}