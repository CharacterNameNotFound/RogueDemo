using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle;
using GameWideSystems.ScriptedVisualEffectManagement;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects
{
    // ToDo: depending on situation, it can make sense to nuke this class and basically call VFX from separate straightforwardly,
    // as using this class as proxy not actually required... Still can be used to register/unregister Story related VFX as pack
    // StoryVisualEffectManager -> StoryVisualEffectRegistrationManager
    public class StoryVisualEffectManager : IStoryVisualEffectManager
    {
        private FlyingTextScriptedVisualEffectRegisterer _flyingTextRegisterer;
        private IScriptedVisualEffectManager _scriptedVisualEffectManager;
        private StoryVisualEffectManagerConfigs _storyVisualEffectManagerConfigs;
        private FlyingParticleScriptedVisualEffectRegisterer _flyingParticleVFXRegisterer;
        private ItemFrameParticlesRegisterer _frameParticlesRegisterer;

        public StoryVisualEffectManager(
            FlyingTextScriptedVisualEffectRegisterer flyingTextRegisterer, 
            IScriptedVisualEffectManager scriptedVisualEffectManager, 
            StoryVisualEffectManagerConfigs storyVisualEffectManagerConfigs, 
            FlyingParticleScriptedVisualEffectRegisterer flyingParticleVFXRegisterer, 
            ItemFrameParticlesRegisterer frameParticlesRegisterer)
        {
            _flyingTextRegisterer = flyingTextRegisterer;
            _scriptedVisualEffectManager = scriptedVisualEffectManager;
            _storyVisualEffectManagerConfigs = storyVisualEffectManagerConfigs;
            _flyingParticleVFXRegisterer = flyingParticleVFXRegisterer;
            _frameParticlesRegisterer = frameParticlesRegisterer;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            await _flyingTextRegisterer.Register(cancellationToken);
            await _flyingParticleVFXRegisterer.Register(cancellationToken);
            await _frameParticlesRegisterer.Register(cancellationToken);
        }

        public UniTask PlayFlyingText(
            Transform parent, 
            Vector3 startingPosition, 
            Vector3 destinationPosition, 
            string text, 
            float fontSize,
            CancellationToken cancellationToken)
        {
            FlyingTextScriptedVisualEffectParams configs = 
                new FlyingTextScriptedVisualEffectParams(
                    parent,
                    startingPosition, 
                    destinationPosition,
                    _storyVisualEffectManagerConfigs.Duration,
                    _storyVisualEffectManagerConfigs.FadeAtPercentile,
                    _storyVisualEffectManagerConfigs.FadeValue,
                    text,
                    fontSize);

            
            return _scriptedVisualEffectManager.Play<FlyingTextScriptedVisualEffect>(configs, cancellationToken);
        }
        
        public UniTask PlayFlyingParticleVFX(FlyingParticleParameters flyingParticleParameters, CancellationToken cancellationToken)
        {
            return _scriptedVisualEffectManager.Play<FlyingParticleScriptedVisualEffect>(flyingParticleParameters, cancellationToken);
        }

        public UniTask<ItemFrameParticles> GetItemFameParticleSystem(ItemFrameParticlesParameters itemFrameParticlesParameters, CancellationToken cancellationToken)
        {
            return _scriptedVisualEffectManager.Get<ItemFrameParticles>(itemFrameParticlesParameters, cancellationToken);
        }

        public void Clear()
        {
            
        }
        
    }
}