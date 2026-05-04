using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using TMPro;
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
        [field: SerializeField] public Collider2D Collider2D { get; private set; }
        [field: SerializeField] public TMP_Text PriceTag { get; private set; }


        public Item StoredItem;

        public override void OnPooled()
        {
            gameObject.SetActive(false);
            
            UpdateInternal(0, true, true);
            
            StoredItem = null;
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

        public void SetPriceTag(float value)
        {
            SetPriceTag(Mathf.CeilToInt(value));
        }
        
        public void SetPriceTag(int value)
        {
            PriceTag.text = value.ToString();
        }
        
        public void RenderUpgradeMovement(int offset = 0)
        {
            UpdateInternal(2 + offset, false, false);
        }
        
        public void RenderStarMovement()
        {
            UpdateInternal(1, false, false);
        }
        
        public void ResetRender()
        {
            UpdateInternal(0, true, true);
        }

        private void UpdateInternal(int layer, bool showTag, bool enableCollider)
        {
            SortingGroup.sortingOrder = layer;
            
            PriceTag.gameObject.SetActive(showTag);
            
            Collider2D.enabled = enableCollider;
        }
        
    }
}