using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySelector.UI.States;
using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;
using GameWideSystems.UIManagement.Screen.StateMachineGeneric;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.GameMode.StorySelector.UI
{
    public class StorySelectorScreenController : GenericSMUIScreen<StorySelectorScreenContext, StorySelectorScreenDependencies, BaseStorySelectorScreenState, IScreenParams>
    {
        [SerializeField] private CharacterSelectionScreenState _characterSelectionScreenState;
        [SerializeField] private StorySelectionScreenState _storySelectionScreenState;
        
        public override ScreenType ScreenType { get; }
        public override ScreenHolderType ScreenHolderType { get; }

        public override async UniTask Initialize(IUIScreenDependencies uiScreenDependencies, CancellationToken cancellationToken)
        {
            await base.Initialize(uiScreenDependencies, cancellationToken);
            
            Context = new StorySelectorScreenContext(
                _characterSelectionScreenState, 
                _storySelectionScreenState, 
                this);
            
            await _characterSelectionScreenState.Initialize(Context, Dependencies, cancellationToken);
            await _storySelectionScreenState.Initialize(Context, Dependencies, cancellationToken);
            
        }

        public override async UniTask OnOpen(CancellationToken cancellationToken)
        {
            await SwitchView(_storySelectionScreenState, cancellationToken);
            await base.OnOpen(cancellationToken);
        }
        
        public async UniTask SwapScreenAsync(BaseStorySelectorScreenState baseStorySelectorScreenState, CancellationToken cancellationToken)
        {
            Dependencies.InputControlFacade.SetInputsAvailable(false);
            
            await SwitchView(baseStorySelectorScreenState, cancellationToken);
            
            Dependencies.InputControlFacade.SetInputsAvailable(true);
        }
        
        
    }
}