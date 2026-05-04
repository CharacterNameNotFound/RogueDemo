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

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling.Handlers
{
    public class DealDamageEffectorHandler : IEffectorHandler
    {
        public Type AutoDictionaryKey => typeof(DealDamageEffector);

        private IHeroStatModificator _heroStatModificator;
        private IStatProviderHandlersRegistry _statProviderHandlersRegistry;
        private ITargetSelectionHandlersRegistry _targetSelectionHandlersRegistry;

        public DealDamageEffectorHandler(
            IHeroStatModificator heroStatModificator, 
            IStatProviderHandlersRegistry statProviderHandlersRegistry, 
            ITargetSelectionHandlersRegistry targetSelectionHandlersRegistry)
        {
            _heroStatModificator = heroStatModificator;
            _statProviderHandlersRegistry = statProviderHandlersRegistry;
            _targetSelectionHandlersRegistry = targetSelectionHandlersRegistry;
        }
        
        public UniTask Handle(Effector effector, int index, int owner, BattleCache battleCache, CancellationToken cancellationToken)
        {
            DealDamageEffector dealDamageEffector = (DealDamageEffector) effector;

            _statProviderHandlersRegistry.Get(dealDamageEffector.DamageStatProvider.GetType(), out IStatProvidingHandler statProvider);

            float value = statProvider.GetValue(dealDamageEffector.DamageStatProvider, index, owner, battleCache);

            _targetSelectionHandlersRegistry.Get(dealDamageEffector.TargetSelector.GetType(), out ITargetSelectionHandler handler);

            int[] targetIndex = handler.GetTargetIndex(dealDamageEffector.TargetSelector, index, owner, battleCache);

            foreach (int target in targetIndex)
            {
                HeroGroup indexToHeroGroup = TargetCalculator.IndexToHeroGroup(target);
                _heroStatModificator.UpdateHp(-value, indexToHeroGroup);
            }
            
            // implement critical 

            
            return UniTask.CompletedTask;
        }

    }
}