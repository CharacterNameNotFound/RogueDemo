using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.Login.UI.Screens
{
    public class CreateProfileViewController : LoginScreenViewController
    {
        [SerializeField] private CanvasGroup _screenRoot;
            
        [SerializeField] private Button _createProfileButton;
        [SerializeField] private Button _cancelCreateProfileButton;
        [SerializeField] private TMP_InputField _newProfileName;
        [SerializeField] private TMP_Text _errorMessage;

        private void Awake()
        {
            _createProfileButton.onClick.AddListener(TryCreateProfile);
            _cancelCreateProfileButton.onClick.AddListener(SwitchToProfileSelect);
        }
        
        

        public override UniTask Show(CancellationToken cancellationToken)
        {
            _screenRoot.interactable = true;
            _errorMessage.gameObject.SetActive(false);
            _newProfileName.text = "";
            
            if (!LoginScreenContext.ProfileList.Any())
            {
                _cancelCreateProfileButton.gameObject.SetActive(false);
            }
            
            return base.Show(cancellationToken);
        }

        private void TryCreateProfile()
        {
            TryCreateProfileAsync(Application.exitCancellationToken).Forget();
        }

        private async UniTask TryCreateProfileAsync(CancellationToken cancellationToken)
        {
            LoginScreenDependencies.InputControlFacade.SetInputsAvailable(false);
            
            string profileName = _newProfileName.text;
            ProcedureResult tryCreateProfile = await LoginScreenDependencies.ProfileCreationRoutine.TryCreateProfile(profileName, cancellationToken);

            if (tryCreateProfile.IsFailure(out string error))
            {
                LoginScreenDependencies.InputControlFacade.SetInputsAvailable(true);
                ReportError(error);
                return;
            }

            await LoginScreenDependencies.LoadingScreenManager.Show(cancellationToken);
            await LoginScreenDependencies.GameStateManager.SwapTopState(LoginScreenDependencies.MainHubGameModeFactory.Create(),
                cancellationToken: Application.exitCancellationToken);
            LoginScreenDependencies.InputControlFacade.SetInputsAvailable(true);
        }
        
        private void SwitchToProfileSelect()
        {
            _screenRoot.interactable = false;
            LoginScreenContext.LogInScreenController.SwitchView(LoginScreenContext.LogInScreenController.SelectExistingProfileView, Application.exitCancellationToken).Forget();
        }

        private void ReportError(string errorText)
        {
            _errorMessage.text = errorText;
            _errorMessage.gameObject.SetActive(true);
        }
         
        
    }
}