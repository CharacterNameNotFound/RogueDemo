using System;
using Game.GameMode.StorySession.Utilities.WorldInteractables;
using UnityEngine;

namespace Game.GameMode.StorySession.Utilities.WorldInteractebles
{
    public class WorldButton : MonoBehaviour, IWorldInteractable
    {
        public event Action OnPress;

        public void Tapped()
        {
            OnPress?.Invoke();
        }
        
    }
}