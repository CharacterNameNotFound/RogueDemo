using GameWideSystems.CameraManagement;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using UnityEngine;
using Utils.UtilityTypes.Counters;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection
{
    public class StorySessionEncounterSelectionInputLayer : IInputHandlerLayer
    {
        public int Index => 1000;
        public InputType InputType => InputType.Pointer;

        private CounterLock _inputLock = new(false);
        
        private ICameraManager _cameraManager;

        public StorySessionEncounterSelectionInputLayer(ICameraManager cameraManager)
        {
            _cameraManager = cameraManager;
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

            if (overlapPoint is null || !overlapPoint.TryGetComponent(out EncounterSelectorEntryComponent component))
            {
                return false;
            }

            component.IsSelected = true;
            
            return true;
        }

        public void SetActive(bool state)
        {
            _inputLock.Toggle(state);
        }
        
    }
}