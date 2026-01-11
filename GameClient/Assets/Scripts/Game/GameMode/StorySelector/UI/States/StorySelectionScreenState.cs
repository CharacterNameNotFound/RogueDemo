using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameMode.StorySelector.UI.States
{
    public class StorySelectionScreenState : BaseStorySelectorScreenState
    {
        [SerializeField] private Button _selectStory;
        [SerializeField] private Button _return;

        public override async UniTask Initialize(StorySelectorScreenContext context, StorySelectorScreenDependencies dependencies,
            CancellationToken cancellationToken)
        {
            await base.Initialize(context, dependencies, cancellationToken);
            
            _selectStory.onClick.AddListener(SwapScreen);
            _return.onClick.AddListener(Return);
        }

        private void SwapScreen()
        {
            Context.StorySelectorScreenController.SwapScreenAsync(Context.CharacterSelectionScreenState, Application.exitCancellationToken).Forget();
        }

        private void Return()
        {
            AsyncReturn(Application.exitCancellationToken).Forget();
        }

        private async UniTask AsyncReturn(CancellationToken cancellationToken)
        {
            Dependencies.InputControlFacade.SetInputsAvailable(false);
            
            await Dependencies.LoadingScreenManager.Show(cancellationToken);
            await Dependencies.GameStateManager.CloseCurrentGameState(true, cancellationToken: cancellationToken);
            
            Dependencies.InputControlFacade.SetInputsAvailable(true);
        }
        
        private void OnDestroy()
        {
            _selectStory.onClick.RemoveAllListeners();
            _return.onClick.RemoveAllListeners();
            
        }
        
        
    }
}