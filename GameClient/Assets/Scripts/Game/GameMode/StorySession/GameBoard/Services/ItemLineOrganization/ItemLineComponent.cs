using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using UnityEngine;
using Utils.UtilityTypes.LifeCycle;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    public class ItemLineComponent : MonoBehaviour, IInitializableGameObject
    {
        public const int MaxItemCapacity = 12;
        
        [field: SerializeField] public int ItemCapacity { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public bool IsPlayerModifyAvailable { get; private set; }

        public ItemContainerComponent[] ItemContainerComponents;

        public float FirstItemX;
        public float StepX;

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            ItemContainerComponents = new ItemContainerComponent[ItemCapacity];

            ItemCapacity = Mathf.Min(MaxItemCapacity, ItemCapacity);

            StepX = SpriteRenderer.bounds.size.x / 12f;
            FirstItemX = SpriteRenderer.bounds.min.x + StepX / 2;
            
            return UniTask.CompletedTask;
        }

        

    }
}