using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class ApplySlowEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(ApplySlowEffector);

        private IItemStatusEffectApplierRegistry _effectRegistry;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;

        public ApplySlowEffectorHandler(
            IItemStatusEffectApplierRegistry effectRegistry, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry)
        {
            _effectRegistry = effectRegistry;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
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
            _effectRegistry.Get(typeof(SlowItemStatusEffect), out IItemStatusEffectApplier hasteApplier);
            
            foreach (int target in targetIndexes)
            {
                Item item = CacheShortcuts.GetItem(target, targetHero, battleCache);
                
                hasteApplier.Apply(item, duration);
            }
            
            return UniTask.CompletedTask;
        }
    }
}