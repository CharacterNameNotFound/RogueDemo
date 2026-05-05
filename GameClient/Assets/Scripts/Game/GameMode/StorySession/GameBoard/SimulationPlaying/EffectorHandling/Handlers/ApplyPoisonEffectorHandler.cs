using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils.Crit;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class ApplyPoisonEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplyPoisonEffector);
        
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private ICriticalApplier _criticalApplier;
        private IHeroStatusEffectHandlerRegistry _effectHandlerRegistry;

        public ApplyPoisonEffectorHandler(
            IHeroStatusEffectHandlerRegistry effectHandlerRegistry, 
            ICriticalApplier criticalApplier, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry)
        {
            _effectHandlerRegistry = effectHandlerRegistry;
            _criticalApplier = criticalApplier;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
        }

        public UniTask Handle(Effector effector, int index, int owner, BattleCache battleCache, CancellationToken cancellationToken)
        {
            ApplyPoisonEffector applyPoison = (ApplyPoisonEffector) effector;
            
            _statProviderHandlersRegistry.Get(applyPoison.PoisonStatProvider.GetType(), out IStatProvidingHandler statProvider);
            float value = statProvider.GetValue(applyPoison.PoisonStatProvider, index, owner, battleCache);
            
            _targetSelectionHandlersRegistry.Get(applyPoison.TargetSelector.GetType(), out ITargetSelectionHandler handler);
            (int[] targetIndex, int targetHero) = handler.GetTargetIndex(applyPoison.TargetSelector, index, owner, battleCache);
            
            value = _criticalApplier.TryApply(value, index, owner, battleCache);
            
            _effectHandlerRegistry.Get(typeof(PoisonHeroStatusEffect), out IHeroStatusEffectHandler result);
            
            result.Apply(targetHero, value, applyPoison.ApplicationType, battleCache);
            
            return UniTask.CompletedTask;
        }
    }
}