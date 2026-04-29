using System;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;

namespace GameWideSystems.TooltipsManagement
{
    public class TooltipInputLayer : IInputHandlerLayer
    {
        public event Action<Press> OnPointerGesture;
            
            
        public int Index => -2;
        public InputType InputType => InputType.Pointer;
        
        
        public bool TryHandle(IGesture gesture)
        {
            if (gesture is Press press)
            {
                OnPointerGesture?.Invoke(press);
            }
            
            // register and hide
            return false;
        }
        
    }
}