using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling.Handlers
{
    public class BattleStartTriggerHandler : ITriggerHandler
    {
        public Type HandledTriggerType => typeof(BattleStartTriggerToken);
        
        private IEffectorHandlersRegistry _effectorHandlersRegistry;
        

        public BattleStartTriggerHandler(IEffectorHandlersRegistry effectorHandlersRegistry)
        {
            _effectorHandlersRegistry = effectorHandlersRegistry;
        }


        public async UniTask HandleTrigger(TriggerToken triggerToken, BattleCache battleCache, CancellationToken cancellationToken)
        {
            // Empty for now, but may pass smth like IsBossFight, etc.
            //BattleStartTriggerToken battleStartTriggerToken = (BattleStartTriggerToken) triggerToken;
            
            foreach (BattleSideCache battleSideCache in battleCache.BattleSideCache)
            {
                foreach (ItemCache item in battleSideCache.ItemCache)
                {
                    await TryHandleForItem(item, battleCache, cancellationToken);
                }
                
            }
            
            
        }

        private UniTask TryHandleForItem(ItemCache item, BattleCache battleCache, CancellationToken cancellationToken)
        {
            OnBattleStartTrigger trigger = (OnBattleStartTrigger) item.Item.Triggers.FirstOrDefault(x => x is OnBattleStartTrigger);

            if (trigger is null)
            {
                return UniTask.CompletedTask;
            }
            
            foreach (Effector effector in trigger.Effectors)
            {
                if (!_effectorHandlersRegistry.Get(effector.GetType(), out IEffectorHandler handler))
                {
                    continue;
                }
                
                handler.Handle(effector, item.Index, item.OwnerIndex, battleCache, cancellationToken).Forget();
            
            }
            
            return UniTask.CompletedTask;
        }
        
        
    }
}