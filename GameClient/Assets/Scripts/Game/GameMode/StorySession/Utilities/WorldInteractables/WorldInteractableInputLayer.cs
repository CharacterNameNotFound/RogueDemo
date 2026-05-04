using GameWideSystems.CameraManagement;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using UnityEngine;
using Utils.UtilityTypes.Counters;

namespace Game.GameMode.StorySession.Utilities.WorldInteractables
{
    public class WorldInteractableInputLayer : IInputHandlerLayer
    {
        public int Index => 100;
        public InputType InputType => InputType.Pointer;

        private ICameraManager _cameraManager;
        
        private CounterLock _inputLock = new(true);
        
        
        public WorldInteractableInputLayer(ICameraManager cameraManager)
        {
            _cameraManager = cameraManager;
        }

        public void SetActive(bool isActive)
        {
            _inputLock.Toggle(isActive);
        } 
        

        public bool TryHandle(IGesture gesture)
        {
            if (_inputLock.IsLocked())
            {
                return false;
            }

            if (gesture is not Tap tap)
            {
                return false;
            }
            
            Vector3 worldPoint = _cameraManager.MainCamera.ScreenToWorldPoint(tap.SourcePosition);
            Collider2D overlapPoint = Physics2D.OverlapPoint(worldPoint);

            if (overlapPoint is null || !overlapPoint.TryGetComponent(out IWorldInteractable worldInteractable))
            {
                return false;
            }
            
            worldInteractable.Tapped();
            
            
            return false;
        }
    }
}