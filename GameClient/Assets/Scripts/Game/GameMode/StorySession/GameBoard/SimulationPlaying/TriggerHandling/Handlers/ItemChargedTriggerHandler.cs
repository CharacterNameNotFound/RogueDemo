using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling.Handlers
{
    public class ItemChargedTriggerHandler : ITriggerHandler
    {
        public Type HandledTriggerType => typeof(ItemChargedTrigger);


        private IEffectorHandlersRegistry _effectorHandlersRegistry;
        

        public ItemChargedTriggerHandler(IEffectorHandlersRegistry effectorHandlersRegistry)
        {
            _effectorHandlersRegistry = effectorHandlersRegistry;
        }


        public async UniTask HandleTrigger(TriggerToken triggerToken, BattleCache battleCache, CancellationToken cancellationToken)
        {
            ItemChargedTrigger itemChargedTrigger = (ItemChargedTrigger)triggerToken;

            Item item = battleCache.Get(itemChargedTrigger.Owner).Model.Items[itemChargedTrigger.Index];

            OnChargedTrigger trigger = (OnChargedTrigger) item.Triggers.First(x => x is OnChargedTrigger);

            foreach (Effector effector in trigger.Effectors)
            {
                if (!_effectorHandlersRegistry.Get(effector.GetType(), out IEffectorHandler handler))
                {
                    continue;
                }
                
                handler.Handle(effector, itemChargedTrigger.Index, itemChargedTrigger.Owner, battleCache, cancellationToken).Forget();
            }
            
            // ToDo: implement Animation (async hero for this reason)
            
            item.ItemStats.IsCharged = false;
            item.ItemStats.CurrentCharge = 0;
            
        }
        
        
        
        
        
    }
}