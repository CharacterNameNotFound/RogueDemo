using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Controller;
using Game.GameMode.StorySession.Data.Story;
using Game.GameMode.StorySession.Services.SaveManagement;
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
        [field: SerializeField] private Button _continueSelectionButton;

        private GeneralSessionSaveData _saveData;
        
        
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;
        
        
        public override async UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            UniTask<UniTask> result = base.OnBeforeOpen(screenParams, cancellationToken);
            
            _playSelectionButton.onClick.AddListener(Play);
            _continueSelectionButton.onClick.AddListener(Continue);

            RequestResult<GeneralSessionSaveData> save = await Dependencies.GeneralSaveManager.ReadSave(cancellationToken);

            bool isReadableSaveData = save.IsSuccess() && Dependencies.GeneralSaveManager.IsReadableSaveData(save.GetValue());
            
            if (isReadableSaveData)
            {
                _saveData = save.GetValue();
            }
            
            // we need update value in case screen is pooled as preloaded
            _continueSelectionButton.gameObject.SetActive(isReadableSaveData);

            return result;
        }


        
        public override void OnAfterClose()
        {
            _playSelectionButton.onClick.RemoveAllListeners();
            _continueSelectionButton.onClick.RemoveAllListeners();

            _saveData = null;
            
            base.OnAfterClose();
        }

        private void Play()
        {
            StartAsync().Forget();
        }

        private void Continue()
        {
            ContinueAsync().Forget();
        }

        private async UniTask StartAsync()
        {
            Dependencies.InputControlFacade.SetInputsAvailable(false);

            _ = await Dependencies.LoadingScreenManager.Show(Application.exitCancellationToken);
            
            await Dependencies.GameStateManager.AppendGameState(
                Dependencies.StorySelectorGameModeFactory.Create(),
                cancellationToken: Application.exitCancellationToken);
            
            Dependencies.InputControlFacade.SetInputsAvailable(true);
        }
        
        private async UniTask ContinueAsync()
        {
            Dependencies.InputControlFacade.SetInputsAvailable(false);

            _ = await Dependencies.LoadingScreenManager.Show(Application.exitCancellationToken);

            StorySessionGameModeInitializationParameters parameters = 
                new StorySessionGameModeInitializationParameters(
                    new StoryStartData(_saveData.ScenarioId, _saveData.CharacterId), 
                    true);
            
            await Dependencies.GameStateManager.AppendGameState(
                Dependencies.StorySessionGameModeFactory.Create(), 
                initializationParameters: parameters,
                cancellationToken: Application.exitCancellationToken);
            
            Dependencies.InputControlFacade.SetInputsAvailable(true);
        }
        
        
    }
}