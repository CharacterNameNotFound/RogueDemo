using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.Login.UI.Screens
{
    public class SelectProfileViewController : LoginScreenViewController
    {
        [SerializeField] private CanvasGroup _screenRoot;
        
        [SerializeField] private Button _switchToCreateProfileView;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _deleteProfile;
        [SerializeField] private TMP_Dropdown _profilesDropdown;

        private void Awake()
        {
            _playButton.onClick.AddListener(Play);
            _switchToCreateProfileView.onClick.AddListener(SwitchToCreateProfile);
            _deleteProfile.onClick.AddListener(RemoveCurrentProfile);
        }
        
        public override UniTask Show(CancellationToken cancellationToken)
        {
            _screenRoot.interactable = true;
            
            RedrawProfileList(LoginScreenContext);
            
            return base.Show(cancellationToken);
        }

        private void RemoveCurrentProfile()
        {
            string selectedProfile = _profilesDropdown.captionText.text;
            Directory.Delete(LoginScreenDependencies.GenericPathProvider.ProfileSaveFilesPath(selectedProfile), true);
            LoginScreenContext.ProfileList.Remove(selectedProfile);

            if (LoginScreenContext.ProfileList.Count == 0)
            {
                SwitchToCreateProfile();
                
                return;
            }

            RedrawProfileList(LoginScreenContext);
        }
        
        private void SwitchToCreateProfile()
        {
            _screenRoot.interactable = false;

            LoginScreenContext
                .LogInScreenController
                .SwitchView(LoginScreenContext.LogInScreenController.CreateProfileView, Application.exitCancellationToken).Forget();
        }

        private void Play()
        {
            PlayAsync(Application.exitCancellationToken).Forget();
        }
        
        private async UniTask PlayAsync(CancellationToken cancellationToken)
        {
            LoginScreenDependencies.InputControlFacade.SetInputsAvailable(false);
            ProcedureResult procedureResult = await LoginScreenDependencies.ProfileLoadingRoutine.TryLoadProfile(_profilesDropdown.captionText.text, cancellationToken);
            if (procedureResult.IsFailure())
            {
                LoginScreenDependencies.InputControlFacade.SetInputsAvailable(true);
                return;
            }
            
            await LoginScreenDependencies.LoadingScreenManager.Show(cancellationToken);
            await LoginScreenDependencies.GameStateManager.SwapTopState(
                LoginScreenDependencies.MainHubGameModeFactory.Create(), 
                true, 
                null, 
                null,
                cancellationToken);
            
            LoginScreenDependencies.InputControlFacade.SetInputsAvailable(true);
        }

        private void RedrawProfileList(LoginScreenContext context)
        {
            _profilesDropdown.ClearOptions();
            _profilesDropdown.AddOptions(context.ProfileList);
            _profilesDropdown.value = context.ProfileList.Count - 1;
        }
        
    }
}