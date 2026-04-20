using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Story;
using Game.Utilities.Constants;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySelector.UI.States
{
    public class StorySelectionScreenState : BaseStorySelectorScreenState
    {
        [SerializeField] private Button _selectStory;
        [SerializeField] private Button _return;

        [Header("Selector")] 
        [SerializeField] private Button _left;
        [SerializeField] private Button _right;
        [SerializeField] private Image _storyImage;
        
        private int _selectedIndex;
        
        private IList<StoryData> _storyDatas;
        
        // needed for Addressable.Release
        private IList<StoryData> _storyDatasHandle;
        
        public override async UniTask Initialize(StorySelectorScreenContext context, StorySelectorScreenDependencies dependencies,
            CancellationToken cancellationToken)
        {
            await base.Initialize(context, dependencies, cancellationToken);

            _selectStory.onClick.AddListener(SwapScreen);
            _return.onClick.AddListener(Return);
            
            _left.onClick.AddListener(() => ChangeToSideAsync(-1, cancellationToken).Forget());
            _right.onClick.AddListener(() => ChangeToSideAsync(1, cancellationToken).Forget());
            
            _storyDatasHandle = await Addressables.LoadAssetsAsync<StoryData>(nameof(AddressableTags.StoryData))
                .ToUniTask(cancellationToken: cancellationToken);
            
            _storyDatas = _storyDatasHandle.OrderBy(item => item.SortingOrder).ToList();
            
            Sprite sprite = await _storyDatas[0].StoryImage.Load<Sprite>(cancellationToken);
            _storyImage.sprite = sprite;
            
            Context.StoryID = _storyDatas[_selectedIndex].StoryId.RuntimeKey.ToString();
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
            
            _left.onClick.RemoveAllListeners();
            _right.onClick.RemoveAllListeners();

            Addressables.Release(_storyImage.sprite);
            Addressables.Release(_storyDatasHandle);
        }
        
        private async UniTask ChangeToSideAsync(int changeValue, CancellationToken cancellationToken)
        {
            ToggleButtons(false);

            int initialValue = _selectedIndex;
            _selectedIndex += changeValue;

            _selectedIndex = Mathf.Clamp(_selectedIndex, 0, _storyDatas.Count - 1);

            if (_selectedIndex == initialValue)
            {
                ToggleButtons(true);
                return;
            }

            Context.StoryID = _storyDatas[_selectedIndex].StoryId.RuntimeKey.ToString();
            await SwapImageToIndex(_storyDatas, _selectedIndex, cancellationToken);
            
            ToggleButtons(true);
        }

        private async UniTask SwapImageToIndex(IList<StoryData> storyDatas, int index, CancellationToken cancellationToken)
        {
            Sprite sprite = await storyDatas[index].StoryImage.Load<Sprite>(cancellationToken);
            
            Addressables.Release(_storyImage.sprite);
            _storyImage.sprite = sprite;
        }

        private void ToggleButtons(bool state)
        {
            _left.interactable = state;
            _right.interactable = state;
        }
        
        
    }
}