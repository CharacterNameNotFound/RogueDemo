using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data;
using Game.Utilities.Constants;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySelector.UI.States
{
    public class CharacterSelectionScreenState : BaseStorySelectorScreenState
    {

        [SerializeField] private Button _selectCharacter;
        [SerializeField] private Button _back;

        [Header("Selector")] 
        [SerializeField] private Button _left;
        [SerializeField] private Button _right;
        [SerializeField] private Image _characterPortrait;

        private int _selectedIndex;
        
        private IList<CharacterData> _characterDatas;
        
        public override async UniTask Initialize(StorySelectorScreenContext context, StorySelectorScreenDependencies dependencies,
            CancellationToken cancellationToken)
        {
            await base.Initialize(context, dependencies, cancellationToken);
            
            _selectCharacter.onClick.AddListener(StartSession);
            _back.onClick.AddListener(SwapScreen);
            
            _left.onClick.AddListener(() => ChangeToSideAsync(-1, Context.CancellationToken).Forget());
            _right.onClick.AddListener(() => ChangeToSideAsync(1, Context.CancellationToken).Forget());

            _characterDatas = await Addressables.LoadAssetsAsync<CharacterData>(nameof(AddressableTags.CharacterData))
                .ToUniTask(cancellationToken: cancellationToken);

            Sprite sprite = await _characterDatas[0].CharacterPortrait.Load<Sprite>(cancellationToken);
            _characterPortrait.sprite = sprite;
            
            Context.CharacterID = _characterDatas[_selectedIndex].CharacterId;
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
            
            _left.onClick.RemoveAllListeners();
            _right.onClick.RemoveAllListeners();

            Addressables.Release(_characterPortrait.sprite);
        }
        
        private async UniTask ChangeToSideAsync(int changeValue, CancellationToken cancellationToken)
        {
            ToggleButtons(false);

            int initialValue = _selectedIndex;
            _selectedIndex += changeValue;

            _selectedIndex = Mathf.Clamp(_selectedIndex, 0, _characterDatas.Count - 1);

            if (_selectedIndex == initialValue)
            {
                ToggleButtons(true);
                return;
            }

            Context.CharacterID = _characterDatas[_selectedIndex].CharacterId;
            await SwapImageToIndex(_characterDatas, _selectedIndex, cancellationToken);
            
            ToggleButtons(true);
        }

        private async UniTask SwapImageToIndex(IList<CharacterData> characterData, int index, CancellationToken cancellationToken)
        {
            Sprite sprite = await characterData[index].CharacterPortrait.Load<Sprite>(cancellationToken);
            if (Context.CancellationToken.IsCancellationRequested)
            {
                return;
            }
            
            Addressables.Release(_characterPortrait.sprite);
            _characterPortrait.sprite = sprite;
        }

        private void ToggleButtons(bool state)
        {
            _left.interactable = state;
            _right.interactable = state;
        }
        
    }
}