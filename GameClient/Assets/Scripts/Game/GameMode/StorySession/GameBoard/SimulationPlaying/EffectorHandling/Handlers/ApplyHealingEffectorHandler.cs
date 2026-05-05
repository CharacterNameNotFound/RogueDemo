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

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class ApplyHealingEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplyHealEffector);

        private IHeroStatModificator _heroStatModificator;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private ICriticalApplier _criticalApplier;

        public ApplyHealingEffectorHandler(
            IHeroStatModificator heroStatModificator, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry, 
            ICriticalApplier criticalApplier)
        {
            _heroStatModificator = heroStatModificator;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
            _criticalApplier = criticalApplier;
        }
        
        public UniTask Handle(Effector effector, int index, int owner, BattleCache battleCache, CancellationToken cancellationToken)
        {
            ApplyHealEffector applyHealingEffector = (ApplyHealEffector) effector;

            // getting damage
            _statProviderHandlersRegistry.Get(applyHealingEffector.HealStatProvider.GetType(), out IStatProvidingHandler statProvider);
            float value = statProvider.GetValue(applyHealingEffector.HealStatProvider, index, owner, battleCache);

            
            // getting target hero
            _targetSelectionHandlersRegistry.Get(applyHealingEffector.TargetSelector.GetType(), out ITargetSelectionHandler handler);
            (int[] targetIndex, int targetHero) = handler.GetTargetIndex(applyHealingEffector.TargetSelector, index, owner, battleCache);

            
            value = _criticalApplier.TryApply(value, index, owner, battleCache);
            

            HeroGroup indexToHeroGroup = TargetCalculator.IndexToHeroGroup(targetHero);
            _heroStatModificator.UpdateHp(value, indexToHeroGroup);
            
            
            return UniTask.CompletedTask;
        }
    }
}