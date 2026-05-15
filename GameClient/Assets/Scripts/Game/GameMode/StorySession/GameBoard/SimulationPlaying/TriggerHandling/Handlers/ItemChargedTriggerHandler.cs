using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling.Handlers
{
    public class ItemChargedTriggerHandler : ITriggerHandler
    {
        public Type HandledTriggerType => typeof(ItemChargedTriggerToken);


        private IEffectorHandlersRegistry _effectorHandlersRegistry;
        private IItemStatGetter _itemStatGetter;
        

        public ItemChargedTriggerHandler(IEffectorHandlersRegistry effectorHandlersRegistry, IItemStatGetter itemStatGetter)
        {
            _effectorHandlersRegistry = effectorHandlersRegistry;
            _itemStatGetter = itemStatGetter;
        }


        public async UniTask HandleTrigger(TriggerToken triggerToken, BattleCache battleCache, CancellationToken cancellationToken)
        {
            ItemChargedTriggerToken itemChargedTriggerToken = (ItemChargedTriggerToken)triggerToken;

            Item item = CacheShortcuts.GetItem(itemChargedTriggerToken.Index, itemChargedTriggerToken.Owner, battleCache);

            OnChargedTrigger trigger = (OnChargedTrigger) item.Triggers.First(x => x is OnChargedTrigger);

            float useCount = _itemStatGetter.GetStatValue(item, ItemStatType.UsageCount);

            useCount = Mathf.CeilToInt(useCount);

            for (int i = 0; i < useCount; i++)
            {
                foreach (Effector effector in trigger.Effectors)
                {
                    if (!_effectorHandlersRegistry.Get(effector.GetType(), out IEffectorHandler handler))
                    {
                        continue;
                    }
                    handler.Handle(effector, itemChargedTriggerToken.Index, itemChargedTriggerToken.Owner, battleCache, cancellationToken).Forget();
                
                }
                
                // ToDo: Pool session rules
                await UniTask.WaitForSeconds(0.25f, cancellationToken: cancellationToken);
            }
            
            
            item.ItemStats.IsCharged = false;
            item.ItemStats.CurrentCharge = 0;
            
        }
        
        
        
        
        
    }
}