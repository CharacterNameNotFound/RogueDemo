using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UITools.GenericViewRoutines;
using GameWideSystems.UIManagement.Screen.StateMachineGeneric;
using UnityEngine;

namespace Game.GameMode.Login.UI.Screens
{
    public abstract class LoginScreenViewController : MonoBehaviour, IUISMGenericState<LoginScreenContext, LogInScreenDependencies>
    {
        protected LoginScreenContext LoginScreenContext;
        protected LogInScreenDependencies  LoginScreenDependencies;

        public UniTask Initialize(LoginScreenContext context, LogInScreenDependencies dependencies, CancellationToken cancellationToken)
        {
            LoginScreenContext = context;
            LoginScreenDependencies = dependencies;
            
            return UniTask.CompletedTask;
        }

        public virtual async UniTask Hide(CancellationToken cancellationToken)
        {
            await GenericUIExitRoutines.HideInstantly(gameObject, cancellationToken);
        }

        public virtual async UniTask Show(CancellationToken cancellationToken)
        {
            await GenericUIEntranceRoutines.ShowInstantly(gameObject, cancellationToken);
        }
        
    }
}