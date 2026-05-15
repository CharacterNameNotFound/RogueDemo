using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.HeroModification;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatModification;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils.Crit;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils.FlyingText;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class UpdateStatValueEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(UpdateStatValueEffector);
        
        private IHeroStatModificator _heroStatModificator;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;
        private ICriticalApplier _criticalApplier;
        private EffectorFlyingTextConfigs _effectorFlyingTextConfigs;
        private ILocalizationManager _localizationManager;
        private IPlayFlyingTextShortcuts _flyingTextShortcuts;
        private IItemStatModificator _itemStatModificator;

        public UpdateStatValueEffectorHandler(
            IHeroStatModificator heroStatModificator, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry, 
            ICriticalApplier criticalApplier, 
            EffectorFlyingTextConfigs effectorFlyingTextConfigs, 
            ILocalizationManager localizationManager, 
            IPlayFlyingTextShortcuts flyingTextShortcuts, 
            IItemStatModificator itemStatModificator)
        {
            _heroStatModificator = heroStatModificator;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
            _criticalApplier = criticalApplier;
            _effectorFlyingTextConfigs = effectorFlyingTextConfigs;
            _localizationManager = localizationManager;
            _flyingTextShortcuts = flyingTextShortcuts;
            _itemStatModificator = itemStatModificator;
        }
        
        
        public UniTask Handle(Effector effector, int index, int owner, BattleCache battleCache, CancellationToken cancellationToken)
        {
            UpdateStatValueEffector updateEffector = (UpdateStatValueEffector) effector;
            
            // getting change value
            _statProviderHandlersRegistry.Get(updateEffector.StatProvider.GetType(), out IStatProvidingHandler statProvider);
            float value = statProvider.GetValue(updateEffector.StatProvider, index, owner, battleCache);
            
            // getting targets
            _targetSelectionHandlersRegistry.Get(updateEffector.TargetSelector.GetType(), out ITargetSelectionHandler targetSelector);
            (int[] targetIndexes, int targetHero) = targetSelector.GetTargetIndex(updateEffector.TargetSelector, index, owner, battleCache);

            foreach (int targetIndex in targetIndexes)
            {
                Item item = CacheShortcuts.GetItem(targetIndex, targetHero, battleCache);
                
                _itemStatModificator.AddStatBaseValue(value, item, updateEffector.ItemStatType, updateEffector.ApplicationType);
            }

            return UniTask.CompletedTask;
        }
        
    }
}