using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Controller;
using Game.GameMode.StorySession.Data;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.Data.Story;
using Game.GameMode.StorySession.Services.SaveManagement;
using Game.Utilities.Constants;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Utils.DiscInteraction;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Result;

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
        
        private IList<CharacterData> _characterDatasHandle;
        private IList<CharacterData> _characterDatas;
        
        public override async UniTask Initialize(StorySelectorScreenContext context, StorySelectorScreenDependencies dependencies,
            CancellationToken cancellationToken)
        {
            await base.Initialize(context, dependencies, cancellationToken);
            
            _selectCharacter.onClick.AddListener( () => StartSession().Forget());
            _back.onClick.AddListener(SwapScreen);
            
            _left.onClick.AddListener(() => ChangeToSideAsync(-1, cancellationToken).Forget());
            _right.onClick.AddListener(() => ChangeToSideAsync(1, cancellationToken).Forget());

            _characterDatasHandle = await Addressables.LoadAssetsAsync<CharacterData>(nameof(AddressableTags.CharacterData))
                .ToUniTask(cancellationToken: cancellationToken);

            _characterDatas = _characterDatasHandle.OrderBy(item => item.Index).ToList();
            
            Sprite sprite = await _characterDatas[0].CharacterPortrait.Load<Sprite>(cancellationToken);
            _characterPortrait.sprite = sprite;
            
            Context.CharacterID = _characterDatas[_selectedIndex].CharacterId;
        }
        
        private async UniTask StartSession()
        {
            StoryStartData storyStartData = new StoryStartData(Context.StoryID, Context.CharacterID);
            StorySessionGameModeInitializationParameters gmParams =
                new StorySessionGameModeInitializationParameters(storyStartData, false);

            await Dependencies.LoadingScreenManager.Show(Application.exitCancellationToken);

            GeneralSessionSaveData generalSessionSaveData = new GeneralSessionSaveData(Context.StoryID, Context.CharacterID, Application.version);
            ProcedureResult result = await Dependencies.GeneralSaveManager.WriteSave(generalSessionSaveData, Application.exitCancellationToken);

            if (result.IsFailure())
            {
                throw result.Exception;
            }
            
            Dependencies.GameStateManager.SwapTopState(
                Dependencies.SessionGameModeFactory.Create(),
                initializationParameters: gmParams,
                cancellationToken: Application.exitCancellationToken)
                .Forget();
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
            
            Addressables.Release(_characterDatasHandle);
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