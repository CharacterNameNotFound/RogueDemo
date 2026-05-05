using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Builders;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    // It is possible to build game entirely relying on ItemContainerComponent
    // But that has massive downside of inability to run simulations without view
    // which is crustal for balancing and autotesting
    // so we're going to use gameboard models as core of simulations, and separating usage of view, so we can use dummy to avoid rendering if needed
    // P.S.: it is not hard but annoying, no metter which way we go -.-
    
    public class SimulationLoop : ISimulationLoop
    {
        private IGameBoardModelHolder _gameBoardModelHolder;
        private ISimulationModelUpdater _simulationModelUpdater;
        private ISimulationViewUpdater _simulationViewUpdater;
        private IWinDecisionMaker _winDecisionMaker;
        private ITriggerProcessor _triggerProcessor;
        private IBattleCacheBuilder _battleCacheBuilder;
        private IItemStatusEffectManager _statusEffectManager;


        // index is index of first item entry in array (both view and model)
        private BattleCache _battleCache;
        
        

        public SimulationLoop(
            IGameBoardModelHolder gameBoardModelHolder, 
            ISimulationModelUpdater simulationModelUpdater,
            ISimulationViewUpdater simulationViewUpdater, 
            IWinDecisionMaker winDecisionMaker, 
            ITriggerProcessor triggerProcessor, 
            IBattleCacheBuilder battleCacheBuilder, 
            IItemStatusEffectManager statusEffectManager)
        {
            _gameBoardModelHolder = gameBoardModelHolder;
            _simulationModelUpdater = simulationModelUpdater;
            _simulationViewUpdater = simulationViewUpdater;
            _winDecisionMaker = winDecisionMaker;
            _triggerProcessor = triggerProcessor;
            _battleCacheBuilder = battleCacheBuilder;
            _statusEffectManager = statusEffectManager;
        }


        public UniTask PrepareSimulationEnvironment(CancellationToken cancellationToken)
        {
            _battleCache = _battleCacheBuilder.BattleCache(_gameBoardModelHolder);
            
            List<ItemCache> playerItemCache = _battleCache.GetPlayer().ItemCache;
            List<ItemCache> encounterItemCache = _battleCache.GetEncounter().ItemCache;
            
            _simulationModelUpdater.ResetChargeValues(playerItemCache);
            _simulationModelUpdater.ResetChargeValues(encounterItemCache);

            _simulationViewUpdater.RenderChargeValues(playerItemCache, encounterItemCache);

            _triggerProcessor.SetCache(_battleCache);
            
            return UniTask.CompletedTask;
        }

        public async UniTask Loop(CancellationToken cancellationToken)
        {
            CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            
            try
            {
                await LoopInternal(cancellationToken);
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
            
        }

        public async UniTask PostSimulationCleanUp(CancellationToken cancellationToken)
        {
            _battleCache = null;
            _triggerProcessor.SetCache(null);
            // reset temporary stats
            
        }


        private async UniTask LoopInternal(CancellationToken cancellationToken)
        {
            TriggerBuffer triggerBuffer = new TriggerBuffer();

            List<ItemCache> playerItemCache = _battleCache.GetPlayer().ItemCache;
            List<ItemCache> encounterItemCache = _battleCache.GetEncounter().ItemCache;

            
            do
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                cancellationToken.ThrowIfCancellationRequested();
                
                triggerBuffer.TransitTriggers();
                
                _simulationModelUpdater.ProgressCharge(playerItemCache, triggerBuffer, Time.deltaTime);
                _simulationModelUpdater.ProgressCharge(encounterItemCache, triggerBuffer, Time.deltaTime);
                    
                _simulationViewUpdater.RenderChargeValues(playerItemCache, encounterItemCache);
                
                _statusEffectManager.Update(playerItemCache, Time.deltaTime);
                _statusEffectManager.Update(encounterItemCache, Time.deltaTime);
                
                _triggerProcessor.Process(triggerBuffer, cancellationToken);

            } while (!_winDecisionMaker.IsWinConditionReached());
        }
        
        
        
        
    }
}