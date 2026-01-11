using Cysharp.Threading.Tasks;
using GameWideSystems.InputManager;
using GameWideSystems.UIManagement;
using QFSW.QC;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Cheats
{
    public class CheatConsoleController
    {
        private IInputHost _inputHost;
        private IScreenHostProvider _screenHostProvider;

        public CheatConsoleController(IInputHost inputHost, IScreenHostProvider screenHostProvider)
        {
            _inputHost = inputHost;
            _screenHostProvider = screenHostProvider;
        }

        public async UniTask Initialize()
        {
            Transform cheatConsoleHolder = _screenHostProvider.CheatConsole;
            
            GameObject console = await Addressables.InstantiateAsync("cheats/cheat_console", new InstantiationParameters(cheatConsoleHolder, false));
            
            QuantumConsole quantumConsole = console.GetComponent<QuantumConsole>();
            quantumConsole.Deactivate();
            
            _inputHost.AddInputLayer(new CheatsInputLayer(quantumConsole));
            
        } 
        
        
    }
}