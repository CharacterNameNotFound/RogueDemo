using System;
using Utils.UtilityTypes.AutoDictionaries;
using Logger = GameWideSystems.Logger.Logger;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling
{
    public class EffectorHandlersRegistry : AutoDictionary<Type, IEffectorHandler>, IEffectorHandlersRegistry
    {
        private Logger _logger;

        public EffectorHandlersRegistry(IEffectorHandler[] entries, Logger logger) : base(entries)
        {
            _logger = logger;
        }

        public bool Get(Type key, out IEffectorHandler result)
        {
            if (TryGet(key, out result)) 
                return true;
            
            _logger.Warn($"Effector of type {key.Name} is missing");
            return false;
        }
        
    }
}