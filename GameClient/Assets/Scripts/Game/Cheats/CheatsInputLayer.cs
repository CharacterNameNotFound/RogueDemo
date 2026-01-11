using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Keyboard;
using QFSW.QC;

namespace Game.Cheats
{
    public class CheatsInputLayer : IInputHandlerLayer
    {
        public int Index => int.MaxValue;
        public InputType InputType => InputType.Keyboard;
        
        private QuantumConsole _console;
        
        public CheatsInputLayer(QuantumConsole console)
        {
            _console = console;
        }
        
        public bool TryHandle(IGesture gesture)
        {
            if (gesture is not KeyboardText text)
            {
                return false;
            }

            if (!_console.IsActive && text.Key == '`')
            {
                _console.Activate();
            }
            
            return true;
        }
    }
}