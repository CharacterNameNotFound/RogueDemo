using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameMode.StorySelector.UI.States
{
    public class CharacterSelectionScreenState : BaseStorySelectorScreenState
    {

        [SerializeField] private Button _selectCharacter;
        [SerializeField] private Button _back;

        public override async UniTask Initialize(StorySelectorScreenContext context, StorySelectorScreenDependencies dependencies,
            CancellationToken cancellationToken)
        {
            await base.Initialize(context, dependencies, cancellationToken);
            
            _selectCharacter.onClick.AddListener(StartSession);
            _back.onClick.AddListener(SwapScreen);
        }
        
        private void StartSession()
        {
            
        }

        private void SwapScreen()
        {
            Context.StorySelectorScreenController.SwapScreenAsync(Context.StorySelectionScreenState, Application.exitCancellationToken).Forget();
        }
        
        private void OnDestroy()
        {
            _selectCharacter.onClick.RemoveAllListeners();
            _back.onClick.RemoveAllListeners();
        }
        
        
        
    }
}