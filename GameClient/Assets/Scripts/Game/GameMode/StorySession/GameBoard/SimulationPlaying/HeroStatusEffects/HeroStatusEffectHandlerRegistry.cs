using System;
using GameWideSystems.Logger;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects
{
    public class HeroStatusEffectHandlerRegistry : AutoDictionary<Type, IHeroStatusEffectHandler>, IHeroStatusEffectHandlerRegistry
    {
        private Logger _logger;
        
        public HeroStatusEffectHandlerRegistry(IHeroStatusEffectHandler[] entries, Logger logger) : base(entries)
        {
            _logger = logger;
        }


        public bool Get(Type key, out IHeroStatusEffectHandler result)
        {
            if (TryGet(key, out result)) 
                return true;
            
            _logger.Warn($"Item status applier of type {key.Name} is missing");
            return false;
        }
    }
}