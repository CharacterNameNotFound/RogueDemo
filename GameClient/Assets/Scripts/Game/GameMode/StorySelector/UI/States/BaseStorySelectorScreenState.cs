using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UITools.GenericViewRoutines;
using GameWideSystems.UIManagement.Screen.StateMachineGeneric;
using UnityEngine;

namespace Game.GameMode.StorySelector.UI.States
{
    public class BaseStorySelectorScreenState : MonoBehaviour, IUISMGenericState<StorySelectorScreenContext, StorySelectorScreenDependencies>
    {
        protected StorySelectorScreenContext Context;
        protected StorySelectorScreenDependencies  Dependencies;
        
        public virtual UniTask Initialize(
            StorySelectorScreenContext context, 
            StorySelectorScreenDependencies dependencies,
            CancellationToken cancellationToken)
        {
            Context = context;
            Dependencies = dependencies;

            return UniTask.CompletedTask;
        }

        public async UniTask Hide(CancellationToken cancellationToken)
        {
            await GenericUIExitRoutines.HideInstantly(gameObject, cancellationToken);
        }

        public async UniTask Show(CancellationToken cancellationToken)
        {
            await GenericUIEntranceRoutines.ShowInstantly(gameObject, cancellationToken);
        }
    }
}