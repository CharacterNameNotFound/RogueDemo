using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.Utilities.WorldInteractables.Awaiters
{
    public static class InteractablePressWaiter
    {
        public static async UniTask WaitForPress(IWorldInteractable interactable, CancellationToken cancellationToken)
        {
            UniTaskCompletionSource completionSource = new UniTaskCompletionSource();

            CancellationTokenRegistration registration = cancellationToken.Register(() => completionSource.TrySetCanceled());
            Action onPressAction = () => completionSource.TrySetResult();
            interactable.OnPress += onPressAction;
            
            try
            {
                await completionSource.Task;
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                registration.DisposeAsync().AsUniTask().Forget();
            }
            finally
            {
                interactable.OnPress -= onPressAction;
            }
            
        } 
        
        
    }
}