using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.Services.InputControl
{
    public interface IInputLayerControlMediator
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public void ToggleItemMovement(bool isActive);
        public void ToggleDetails(bool isActive);
        public void ToggleEncounter(bool isActive);
        public void ToggleWorldInteractables(bool isActive);
        public UniTask CleanUp(CancellationToken cancellationToken);
    }
}