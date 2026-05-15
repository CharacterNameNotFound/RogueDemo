using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.ScriptedVisualEffectManagement;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle
{
    public class ItemFrameParticlesRegisterer
    {
        private IPooledObjectHostProvider _pooledObjectHostProvider;
        private IScriptedVisualEffectManager _scriptedVisualEffectManager;
        private ItemFrameParticlesConfigs _configs;
        
        public ItemFrameParticlesRegisterer(
            IPooledObjectHostProvider pooledObjectHostProvider, 
            IScriptedVisualEffectManager scriptedVisualEffectManager, 
            ItemFrameParticlesConfigs configs)
        {
            _pooledObjectHostProvider = pooledObjectHostProvider;
            _scriptedVisualEffectManager = scriptedVisualEffectManager;
            _configs = configs;
        }

        
        public UniTask Register(CancellationToken cancellationToken)
        {
            ScriptedVisualEffectPool pool = new ScriptedVisualEffectPool(new List<ScriptedVisualEffectBase>(),
                new AddressablePoolEntityProvider<ScriptedVisualEffectBase>(_configs.Prefab),
                _pooledObjectHostProvider);

            
            return _scriptedVisualEffectManager.RegisterEffect<ItemFrameParticles>(_configs.PooledCount, pool, cancellationToken);
        }
    }
}