using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects.ItemStatusEffectVFXApplication;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils.FlyingParticle;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class ApplySlowEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplySlowEffector);

        private IItemStatusEffectApplierRegistry _effectRegistry;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private IFlyingParticleShortcuts _flyingParticleShortcuts;
        private IItemFrameParticleShortcuts _itemFrameParticleShortcuts;
        private IItemStatusEffectVFXApplier _statusEffectVFXApplier;

        public ApplySlowEffectorHandler(
            IItemStatusEffectApplierRegistry effectRegistry, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry, 
            IFlyingParticleShortcuts flyingParticleShortcuts, 
            IItemFrameParticleShortcuts itemFrameParticleShortcuts, 
            IItemStatusEffectVFXApplier statusEffectVFXApplier)
        {
            _effectRegistry = effectRegistry;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
            _flyingParticleShortcuts = flyingParticleShortcuts;
            _itemFrameParticleShortcuts = itemFrameParticleShortcuts;
            _statusEffectVFXApplier = statusEffectVFXApplier;
        }

        public UniTask Handle(Effector effector, int index, int owner, BattleCache battleCache, CancellationToken cancellationToken)
        {
            ApplySlowEffector slowEffector = (ApplySlowEffector) effector;
            
            // getting duration
            _statProviderHandlersRegistry.Get(slowEffector.SlowDurationProvider.GetType(), out IStatProvidingHandler statProvider);
            float duration = statProvider.GetValue(slowEffector.SlowDurationProvider, index, owner, battleCache);
            
            
            // getting targets
            _targetSelectionHandlersRegistry.Get(slowEffector.TargetSelector.GetType(), out ITargetSelectionHandler targetSelector);
            (int[] targetIndexes, int targetHero) = targetSelector.GetTargetIndex(slowEffector.TargetSelector, index, owner, battleCache);

            
            // getting applier
            _effectRegistry.Get(typeof(SlowItemStatusEffect), out IItemStatusEffectApplier slowStatusEffectApplier);
            
            foreach (int target in targetIndexes)
            {
                Item item = CacheShortcuts.GetItem(target, targetHero, battleCache);
                
                bool isApplied = slowStatusEffectApplier.Apply(item, duration);

                _flyingParticleShortcuts.PlaySlowParticle(index, owner, target, targetHero, cancellationToken);
                
                if (isApplied)
                {
                    _statusEffectVFXApplier.ApplyItemFrameParticles<SlowItemStatusEffect>(EffectGetter, target, targetHero, cancellationToken);
                }
            }
            
            return UniTask.CompletedTask;
        }
        
        private UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)> EffectGetter(int index, int owner, CancellationToken cancellationToken)
        {
            return _itemFrameParticleShortcuts.GetSlowParticles(index, owner, cancellationToken);
        }
        
    }
}