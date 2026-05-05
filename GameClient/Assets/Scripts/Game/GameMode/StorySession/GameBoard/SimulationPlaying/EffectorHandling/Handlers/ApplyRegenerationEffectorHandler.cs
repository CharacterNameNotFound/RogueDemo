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
    public class ApplyRegenerationEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplyRegenerationEffector);
        
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private ICriticalApplier _criticalApplier;
        private IHeroStatusEffectHandlerRegistry _effectHandlerRegistry;

        public ApplyRegenerationEffectorHandler(
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
            ApplyRegenerationEffector applyRegeneration = (ApplyRegenerationEffector) effector;
            
            _statProviderHandlersRegistry.Get(applyRegeneration.RegenerationStatProvider.GetType(), out IStatProvidingHandler statProvider);
            float value = statProvider.GetValue(applyRegeneration.RegenerationStatProvider, index, owner, battleCache);
            
            _targetSelectionHandlersRegistry.Get(applyRegeneration.TargetSelector.GetType(), out ITargetSelectionHandler handler);
            (int[] targetIndex, int targetHero) = handler.GetTargetIndex(applyRegeneration.TargetSelector, index, owner, battleCache);
            
            value = _criticalApplier.TryApply(value, index, owner, battleCache);
            
            _effectHandlerRegistry.Get(typeof(RegenerationHeroStatusEffect), out IHeroStatusEffectHandler result);
            
            result.Apply(targetHero, value, applyRegeneration.ApplicationType, battleCache);
            
            return UniTask.CompletedTask;
        }
    }
}