using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.HeroModification;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Builders;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Builders;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects;
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
        private IItemStatusEffectManager _itemStatusEffectManager;
        private IHeroStatusEffectManager _heroStatusEffectManager; 
        

        // index is index of first item entry in array (both view and model)
        private BattleCache _battleCache;
        
        

        public SimulationLoop(
            IGameBoardModelHolder gameBoardModelHolder, 
            ISimulationModelUpdater simulationModelUpdater,
            ISimulationViewUpdater simulationViewUpdater, 
            IWinDecisionMaker winDecisionMaker, 
            ITriggerProcessor triggerProcessor, 
            IBattleCacheBuilder battleCacheBuilder, 
            IItemStatusEffectManager itemStatusEffectManager, 
            IHeroStatusEffectManager heroStatusEffectManager)
        {
            _gameBoardModelHolder = gameBoardModelHolder;
            _simulationModelUpdater = simulationModelUpdater;
            _simulationViewUpdater = simulationViewUpdater;
            _winDecisionMaker = winDecisionMaker;
            _triggerProcessor = triggerProcessor;
            _battleCacheBuilder = battleCacheBuilder;
            _itemStatusEffectManager = itemStatusEffectManager;
            _heroStatusEffectManager = heroStatusEffectManager;
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
            await _heroStatusEffectManager.PostBattleReset(_battleCache, cancellationToken);
            await _itemStatusEffectManager.PostBattleReset(_battleCache, cancellationToken);
            
            ClearItemsTempStats();
            
            _battleCache = null;
            _triggerProcessor.SetCache(null);
        }


        private async UniTask LoopInternal(CancellationToken cancellationToken)
        {
            TriggerBuffer triggerBuffer = new TriggerBuffer();

            List<ItemCache> playerItemCache = _battleCache.GetPlayer().ItemCache;
            List<ItemCache> encounterItemCache = _battleCache.GetEncounter().ItemCache;
            
            // As we're handling last frame Tokens, and on start we will make swap, pushing it on top
            triggerBuffer.AddTrigger(TriggerTokenBuilder.BattleStartTriggerToken());
            
            do
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                cancellationToken.ThrowIfCancellationRequested();
                
                triggerBuffer.TransitTriggers();
                
                _simulationModelUpdater.ProgressCharge(playerItemCache, triggerBuffer, Time.deltaTime);
                _simulationModelUpdater.ProgressCharge(encounterItemCache, triggerBuffer, Time.deltaTime);
                    
                _simulationViewUpdater.RenderChargeValues(playerItemCache, encounterItemCache);
                
                _itemStatusEffectManager.Update(playerItemCache, Time.deltaTime);
                _itemStatusEffectManager.Update(encounterItemCache, Time.deltaTime);

                // ToDo: Swap Application.exitCancellationToken to game mode cancellation
                _heroStatusEffectManager.Update(_battleCache.GetPlayer(), (int) OwnerIndex.Player, Time.deltaTime, Application.exitCancellationToken);
                _heroStatusEffectManager.Update(_battleCache.GetEncounter(), (int) OwnerIndex.Encounter, Time.deltaTime, Application.exitCancellationToken);
                
                _triggerProcessor.Process(triggerBuffer, cancellationToken);

            } while (!_winDecisionMaker.IsWinConditionReached());
        }

        private void ClearItemsTempStats()
        {
            foreach (BattleSideCache battleSideCache in _battleCache.BattleSideCache)
            {
                foreach (ItemCache itemCache in battleSideCache.ItemCache)
                {
                    itemCache.Item.ItemStats.ClearPostBattle();
                    
                }
            }
        }
        
        
    }
}