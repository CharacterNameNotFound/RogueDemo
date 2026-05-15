using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.ScriptedVisualEffectManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle
{
    //Haste and Slow particle effects are controlled from corresponding systems, so we do not need to control duration from VFX
    public class ItemFrameParticles : ScriptedVisualEffectBase
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private ScriptedVisualEffectParams _parameters;
        
        public override UniTask Play(ScriptedVisualEffectParams parameters, CancellationToken cancellationToken)
        {
            _parameters = parameters;
            
            ItemFrameParticlesParameters particleParams = (ItemFrameParticlesParameters) parameters;

            ParticleSystem.ShapeModule particleSystemShape = _particleSystem.shape;
            particleSystemShape.scale = particleParams.ShapeScale;

            ParticleSystem.MainModule particleSystemMain = _particleSystem.main;
            particleSystemMain.startColor = particleParams.Color;
            
            gameObject.SetActive(true);
            _particleSystem.Play(true);

            return UniTask.CompletedTask;
        }

        public void Return()
        {
            _particleSystem.Stop(true);
            _parameters.Manager.ReturnEffect(this);
        }

        public override void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }
        
    }
}