using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling.Handlers;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TriggerHandling
{
    // Trigger processing can be redesigned with prebuilding item by idem handling pipeline, so we will not query dictionaries so much
    // But that will make some effects more complicated, for example item transformation;
    public class TriggerProcessor : ITriggerProcessor
    {
        private Dictionary<Type, ITriggerHandler> _triggerHandlers;

        private BattleCache _battleCache;
        
        public TriggerProcessor(ITriggerHandler[] triggerHandlers)
        {
            _triggerHandlers = triggerHandlers.ToDictionary(item => item.HandledTriggerType);
        }

        public void SetCache(BattleCache battleCache)
        {
            _battleCache = battleCache;
        }

        public void Process(TriggerBuffer triggerBuffer, CancellationToken cancellationToken)
        {
            foreach (TriggerToken trigger in triggerBuffer.LastFrameTriggers)
            {
                _triggerHandlers[trigger.GetType()].HandleTrigger(trigger, _battleCache, cancellationToken).Forget();
            }
            
        }
    }
}