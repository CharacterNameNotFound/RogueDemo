using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    // It is possible to build game entirely relying on ItemContainerComponent
    // But that has massive downside of inability to run simulations without view
    // which is crustal for balancing and autotesting
    // so we're going to use gameboard models as core of simulations, and separating usage of view, so we can use dummy to avoid rendering if needed
    // ---------------------------
    // I would generally use command patten and pull action tokens from items, while it is much more secure, it is inflexible at same time
    // This type of games generally prefers to have as much flexibility as possible for both animation execution and visual effects
    //---------------------------
    // PS.: it is not hard but annoying, no metter which way we go -.-
    
    public class SimulationLoop : ISimulationLoop
    {
        private IGameBoardModelHolder _gameBoardModelHolder;
        private ISimulationModelUpdater _simulationModelUpdater;
        private ISimulationViewUpdater _simulationViewUpdater;
        private IWinDecisionMaker _winDecisionMaker;


        // index is index of first item entry in array (both view and model)
        private List<ItemCache> _playerItems;
        private List<ItemCache> _encounterItems;

        public SimulationLoop(
            IGameBoardModelHolder gameBoardModelHolder, 
            ISimulationModelUpdater simulationModelUpdater,
            ISimulationViewUpdater simulationViewUpdater, 
            IWinDecisionMaker winDecisionMaker)
        {
            _gameBoardModelHolder = gameBoardModelHolder;
            _simulationModelUpdater = simulationModelUpdater;
            _simulationViewUpdater = simulationViewUpdater;
            _winDecisionMaker = winDecisionMaker;
        }


        public UniTask PrepareSimulationEnvironment(CancellationToken cancellationToken)
        {
            _playerItems = ReadItemsIntoCache(_gameBoardModelHolder.GameBoardModel.PlayerBoard.Items, (int) OwnerIndex.Player);
            _encounterItems = ReadItemsIntoCache(_gameBoardModelHolder.GameBoardModel.EncounterBoard.Items, (int) OwnerIndex.Encounter);

            _simulationModelUpdater.ResetChargeValues(_playerItems);
            _simulationModelUpdater.ResetChargeValues(_encounterItems);

            _simulationViewUpdater.RenderChargeValues(_playerItems, _encounterItems);
            
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
            
            
            return;
        }

        public async UniTask PostSimulationCleanUp(CancellationToken cancellationToken)
        {
            _playerItems = null;
            _encounterItems = null;
            
            throw new NotImplementedException();
        }


        private async UniTask LoopInternal(CancellationToken cancellationToken)
        {
            TriggerBuffer triggerBuffer = new TriggerBuffer();
            
            do
            {
                await UniTask.NextFrame(cancellationToken);
                triggerBuffer.TransitTriggers();
                
                _simulationModelUpdater.ProgressCharge(_playerItems, triggerBuffer);
                _simulationModelUpdater.ProgressCharge(_encounterItems, triggerBuffer);
                    
                _simulationViewUpdater.RenderChargeValues(_playerItems, _encounterItems);

            } while (!_winDecisionMaker.IsWinConditionReached());
        }
        

        private List<ItemCache> ReadItemsIntoCache(Item[] itemLine, int itemOwner)
        {
            List<ItemCache> result = new List<ItemCache>();
            
            for (int i = 0; i < itemLine.Length;)
            {
                if (itemLine[i] is null)
                {
                    i++;
                    continue;
                }

                ItemCache item = new ItemCache(itemLine[i], i, itemOwner);
                result.Add(item);

                i += itemLine[i].ItemSize;
            }

            return result;
        }
        
        
    }
}