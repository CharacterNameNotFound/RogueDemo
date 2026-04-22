using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Configurations;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using UnityEngine;
using Utils.UtilityTypes.LifeCycle;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Views
{
    public class ItemLineComponent : MonoBehaviour, IInitializableGameObject
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public bool IsPlayerModifyAvailable { get; private set; }
        public int ItemCapacity => ItemConfigurations.ItemCapacity;
        

        public ItemContainerComponent[] ItemContainerComponents;

        public float FirstItemX;
        public float StepX;

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            ItemContainerComponents = new ItemContainerComponent[ItemCapacity];

            StepX = SpriteRenderer.bounds.size.x / ItemCapacity;
            FirstItemX = SpriteRenderer.bounds.min.x + StepX / 2;
            
            return UniTask.CompletedTask;
        }

        

    }
}