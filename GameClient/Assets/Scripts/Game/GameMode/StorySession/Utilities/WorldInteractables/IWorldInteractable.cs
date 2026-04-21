using System;

namespace Game.GameMode.StorySession.Utilities.WorldInteractables
{
    public interface IWorldInteractable
    {
        public event Action OnPress;
        
        public void Tapped();
    }
}