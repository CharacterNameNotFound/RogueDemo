using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using UnityEngine;
using Utils.UtilityTypes.LifeCycle;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    public class ItemLineComponent : MonoBehaviour, IInitializableGameObject
    {
        [field: SerializeField] public int ItemCapacity { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        public ItemContainerComponent[] ItemContainerComponents;

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            ItemContainerComponents = new ItemContainerComponent[ItemCapacity];
            
            return UniTask.CompletedTask;
        }


    }
}