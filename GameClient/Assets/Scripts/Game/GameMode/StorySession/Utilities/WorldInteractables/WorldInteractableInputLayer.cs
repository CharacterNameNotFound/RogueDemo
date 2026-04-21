using System;
using System.Text;
using GameWideSystems.CameraManagement;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using UnityEngine;

namespace Game.GameMode.StorySession.Utilities.WorldInteractables
{
    public class WorldInteractableInputLayer : IInputHandlerLayer
    {
        public int Index => 100;
        public InputType InputType => InputType.Pointer;

        private ICameraManager _cameraManager;


        private bool _isActive;
        
        public WorldInteractableInputLayer(ICameraManager cameraManager)
        {
            _cameraManager = cameraManager;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        } 
        

        public bool TryHandle(IGesture gesture)
        {
            if (!_isActive)
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