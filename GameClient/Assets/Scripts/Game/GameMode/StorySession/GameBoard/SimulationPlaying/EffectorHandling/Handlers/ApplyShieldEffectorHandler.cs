using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.HeroModification;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils.Crit;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class ApplyShieldEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplyShieldEffector);

        private IHeroStatModificator _heroStatModificator;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private ICriticalApplier _criticalApplier;
        private EffectorFlyingTextConfigs _effectorFlyingTextConfigs;
        private ILocalizationManager _localizationManager;
        private IPlayFlyingTextShortcuts _flyingTextShortcuts;

        public ApplyShieldEffectorHandler(
            IHeroStatModificator heroStatModificator, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry, 
            ICriticalApplier criticalApplier, 
            EffectorFlyingTextConfigs effectorFlyingTextConfigs, 
            ILocalizationManager localizationManager, 
            IPlayFlyingTextShortcuts flyingTextShortcuts)
        {
            _heroStatModificator = heroStatModificator;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
            _criticalApplier = criticalApplier;
            _effectorFlyingTextConfigs = effectorFlyingTextConfigs;
            _localizationManager = localizationManager;
            _flyingTextShortcuts = flyingTextShortcuts;
        }
        
        public UniTask Handle(Effector effector, int index, int owner, BattleCache battleCache, CancellationToken cancellationToken)
        {
            ApplyShieldEffector shieldEffector = (ApplyShieldEffector) effector;

            // getting damage
            _statProviderHandlersRegistry.Get(shieldEffector.ShieldStatProvider.GetType(), out IStatProvidingHandler statProvider);
            float value = statProvider.GetValue(shieldEffector.ShieldStatProvider, index, owner, battleCache);

            
            // getting target hero
            _targetSelectionHandlersRegistry.Get(shieldEffector.TargetSelector.GetType(), out ITargetSelectionHandler handler);
            (int[] targetIndex, int targetHero) = handler.GetTargetIndex(shieldEffector.TargetSelector, index, owner, battleCache);
            
            
            value = _criticalApplier.TryApply(value, index, owner, battleCache, out bool isCrit);
            

            HeroGroup indexToHeroGroup = TargetCalculator.IndexToHeroGroup(targetHero);
            _heroStatModificator.AddShield(value, indexToHeroGroup);
            
            string text = _localizationManager.GetLocalized(_effectorFlyingTextConfigs.ApplyShield, value);

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