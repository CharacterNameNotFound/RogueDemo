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
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class ApplyRegenerationEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplyRegenerationEffector);
        
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private ICriticalApplier _criticalApplier;
        private IHeroStatusEffectHandlerRegistry _effectHandlerRegistry;
        private EffectorFlyingTextConfigs _effectorFlyingTextConfigs;
        private ILocalizationManager _localizationManager;
        private IPlayFlyingTextShortcuts _flyingTextShortcuts;

        public ApplyRegenerationEffectorHandler(
            IHeroStatusEffectHandlerRegistry effectHandlerRegistry, 
            ICriticalApplier criticalApplier, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry, 
            EffectorFlyingTextConfigs effectorFlyingTextConfigs, 
            ILocalizationManager localizationManager, 
            IPlayFlyingTextShortcuts flyingTextShortcuts)
        {
            _effectHandlerRegistry = effectHandlerRegistry;
            _criticalApplier = criticalApplier;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
            _effectorFlyingTextConfigs = effectorFlyingTextConfigs;
            _localizationManager = localizationManager;
            _flyingTextShortcuts = flyingTextShortcuts;
        }

        public UniTask Handle(Effector effector, int index, int owner, BattleCache battleCache, CancellationToken cancellationToken)
        {
            ApplyRegenerationEffector applyRegeneration = (ApplyRegenerationEffector) effector;
            
            _statProviderHandlersRegistry.Get(applyRegeneration.RegenerationStatProvider.GetType(), out IStatProvidingHandler statProvider);
            float value = statProvider.GetValue(applyRegeneration.RegenerationStatProvider, index, owner, battleCache);
            
            _targetSelectionHandlersRegistry.Get(applyRegeneration.TargetSelector.GetType(), out ITargetSelectionHandler handler);
            (int[] targetIndex, int targetHero) = handler.GetTargetIndex(applyRegeneration.TargetSelector, index, owner, battleCache);
            
            value = _criticalApplier.TryApply(value, index, owner, battleCache, out bool isCrit);
            
            _effectHandlerRegistry.Get(typeof(RegenerationHeroStatusEffect), out IHeroStatusEffectHandler result);
            
            result.Apply(targetHero, value, applyRegeneration.ApplicationType, battleCache, cancellationToken).Forget();
            
            string text = _localizationManager.GetLocalized(_effectorFlyingTextConfigs.ApplyRegeneration, value);

            _flyingTextShortcuts.PlayFlyingTextAtItemPosition(
                index, 
                owner, 
                _effectorFlyingTextConfigs.FlightTrajectory,
                text, 
                isCrit,
                cancellationToken).Forget();
            
            return UniTask.CompletedTask;
        }
    }
}