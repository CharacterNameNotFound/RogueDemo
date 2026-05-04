using System;
using GameWideSystems.Logger;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding
{
    public class StatProviderHandlersRegistry : AutoDictionary<Type, IStatProvidingHandler>, IStatProviderHandlersRegistry
    {
        private Logger _logger;
        
        public StatProviderHandlersRegistry(IStatProvidingHandler[] entries, Logger logger) : base(entries)
        {
            _logger = logger;
        }

        public bool Get(Type key, out IStatProvidingHandler result)
        {
            if (TryGet(key, out result)) 
                return true;
            
            _logger.Warn($"Stat provider of type {key.Name} is missing");
            return false;

        }
    }
}