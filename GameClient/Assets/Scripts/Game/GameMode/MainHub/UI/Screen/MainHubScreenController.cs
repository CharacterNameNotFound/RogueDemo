using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.GameSceneManager;
using GameWideSystems.InputManager;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.UI;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubScreenController : UIScreen<IScreenParams, MainHubScreenDependencies>
    {
        [field: SerializeField] private Button _playSelectionButton;
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;

        public override UniTask Initialize(IUIScreenDependencies uiScreenDependencies, CancellationToken cancellationToken)
        {
            _playSelectionButton.onClick.AddListener(Play);
            return base.Initialize(uiScreenDependencies, cancellationToken);
        }

        public override void Dispose()
        {
            _playSelectionButton.onClick.RemoveAllListeners();
            base.Dispose();
        }

        private void Play()
        {
            StartAsync().Forget();
        }

        private async UniTask StartAsync()
        {
            Dependencies.InputControlFacade.SetInputsAvailable(false);
            ProcedureResult tryLoadBlob = await Dependencies.BlobManager.CreateNew(Application.exitCancellationToken);
            
            if (tryLoadBlob.IsFailure())
            {
                Dependencies.InputControlFacade.SetInputsAvailable(true);
                return;
            }

            _ = await Dependencies.LoadingScreenManager.Show(Application.exitCancellationToken);
            
            await Dependencies.GameStateManager.AppendGameState(
                Dependencies.StorySelectorGameModeFactory.Create(),
                cancellationToken: Application.exitCancellationToken);
            
            Dependencies.InputControlFacade.SetInputsAvailable(true);
        }


        
    }
}