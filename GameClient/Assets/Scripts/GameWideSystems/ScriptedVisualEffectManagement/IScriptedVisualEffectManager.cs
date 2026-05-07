using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameWideSystems.ScriptedVisualEffectManagement
{
    public interface IScriptedVisualEffectManager
    {
        public UniTask RegisterEffect<T>(int preparedEffects, ScriptedVisualEffectPool scriptedVisualEffectPool, CancellationToken cancellationToken) where T : ScriptedVisualEffectBase;
        public UniTask PlayOnParent<T>(Transform parent, ScriptedVisualEffectParams scriptedVisualParams, CancellationToken cancellationToken) where T : ScriptedVisualEffectBase;
        public void ReturnEffect<T>(T effect) where T : ScriptedVisualEffectBase;
        public void Unregister<T>();
    }
}