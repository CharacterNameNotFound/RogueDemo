using UnityEngine;
using UnityEngine.Rendering;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemContainers
{
    public class ItemContainerComponent : PoolableGameObject
    {
        [field: SerializeField] public int Size { get; private set; }
        [field: SerializeField] public SpriteRenderer ItemRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer FrameRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer ChargeProgressRenderer { get; private set; }
        [field: SerializeField] public SortingGroup SortingGroup { get; private set; }
        

        public override void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            
        }
        
    }
}