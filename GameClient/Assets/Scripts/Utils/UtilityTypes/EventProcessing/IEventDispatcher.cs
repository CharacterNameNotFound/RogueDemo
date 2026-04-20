using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.EventProcessing
{
    public interface IEventDispatcher<T> where T : EventArgs
    {
        public void RegisterHandler(Func<T, CancellationToken, UniTask> eventHandler);
        public UniTask Invoke(T args, CancellationToken cancellationToken);
        public void Unregister(Func<T, CancellationToken, UniTask> eventHandler);
        public void CleanUp();
    }
}