using System.IO;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UITools.GenericViewRoutines;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using GameWideSystems.UIManagement.Screen.StateMachineGeneric;
using UnityEngine;

namespace Game.GameMode.Login.UI.Screens
{
    public class LogInScreenController : GenericSMUIScreen <LoginScreenContext, LogInScreenDependencies, LoginScreenViewController, IScreenParams>
    {

        [Header("Views")]
        [field: SerializeField] public CreateProfileViewController CreateProfileView { get; private set; }
        [field: SerializeField] public SelectProfileViewController SelectExistingProfileView { get; private set; }
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;

        public override async UniTask Initialize(IUIScreenDependencies uiScreenDependencies, CancellationToken cancellationToken)
        {
            await base.Initialize(uiScreenDependencies, cancellationToken);
            
            LoadContext();

            await CreateProfileView.Initialize(Context, Dependencies, cancellationToken);
            await SelectExistingProfileView.Initialize(Context, Dependencies, cancellationToken);
        }

        public override async UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            return await base.OnBeforeOpen(screenParams, cancellationToken);
        }

        public override async UniTask OnOpen(CancellationToken cancellationToken)
        {
            await GenericUIEntranceRoutines.ShowInstantly(gameObject, cancellationToken);
            
            LoginScreenViewController viewController =
                Context.ProfileList.Any() ? SelectExistingProfileView : CreateProfileView;

            await SwitchView(viewController, cancellationToken);
        }

        public override UniTask OnClose(CancellationToken cancellationToken)
        {
            return GenericUIExitRoutines.HideInstantly(gameObject, cancellationToken);
        }

        public override UniTask OnCloseSilently(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
        
        private void LoadContext()
        {
            DirectoryInfo directory = new DirectoryInfo(Dependencies.GenericPathProvider.SaveFilesPath());
            
            Context = new LoginScreenContext(
                this,
                directory.GetDirectories().Select(item => item.Name).ToList()
                );
        }
        
    }
}