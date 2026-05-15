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
    public class ApplyHasteEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplyHasteEffector);

        private IItemStatusEffectApplierRegistry _effectRegistry;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private IFlyingParticleShortcuts _flyingParticleShortcuts;
        private IItemFrameParticleShortcuts _itemFrameParticleShortcuts;
        private IItemStatusEffectVFXApplier _statusEffectVFXApplier; 

        public ApplyHasteEffectorHandler(
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
            ApplyHasteEffector hasteEffector = (ApplyHasteEffector) effector;
            
            // getting duration
            _statProviderHandlersRegistry.Get(hasteEffector.HasteDurationProvider.GetType(), out IStatProvidingHandler statProvider);
            float duration = statProvider.GetValue(hasteEffector.HasteDurationProvider, index, owner, battleCache);
            
            
            // getting targets
            _targetSelectionHandlersRegistry.Get(hasteEffector.TargetSelector.GetType(), out ITargetSelectionHandler targetSelector);
            (int[] targetIndexes, int targetHero) = targetSelector.GetTargetIndex(hasteEffector.TargetSelector, index, owner, battleCache);

            // getting applier
            _effectRegistry.Get(typeof(HasteItemStatusEffect), out IItemStatusEffectApplier hasteApplier);
            
            foreach (int target in targetIndexes)
            {
                Item item = CacheShortcuts.GetItem(target, targetHero, battleCache);
                
                bool isApplied = hasteApplier.Apply(item, duration);
                
                _flyingParticleShortcuts.PlayHasteParticle(index, owner, target, targetHero, cancellationToken);

                if (isApplied)
                {
                    _statusEffectVFXApplier.ApplyItemFrameParticles<HasteItemStatusEffect>(EffectGetter, target, targetHero, cancellationToken);
                }
            }
            
            return UniTask.CompletedTask;
        }
        
        private UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)> EffectGetter(int index, int owner, CancellationToken cancellationToken)
        {
            return _itemFrameParticleShortcuts.GetHasteParticles(index, owner, cancellationToken);
        }
        
    }
}