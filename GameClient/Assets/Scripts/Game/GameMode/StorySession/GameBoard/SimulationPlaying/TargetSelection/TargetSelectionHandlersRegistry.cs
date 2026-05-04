using System;
using GameWideSystems.Logger;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection
{
    public class TargetSelectionHandlersRegistry : AutoDictionary<Type, ITargetSelectionHandler>, ITargetSelectionHandlersRegistry
    {
        private Logger _logger;
        
        public TargetSelectionHandlersRegistry(ITargetSelectionHandler[] entries, Logger logger) : base(entries)
        {
            _logger = logger;
        }

        public bool Get(Type key, out ITargetSelectionHandler result)
        {
            if (TryGet(key, out result)) 
                return true;
            
            _logger.Warn($"TargetSelector of type {key.Name} is missing");
            return false;
        }
    }
}