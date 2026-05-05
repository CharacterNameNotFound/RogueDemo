using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects
{
    public interface IHeroStatusEffectHandlerRegistry
    {
        /// <param name="key"> IHeroStatusEffect type </param>
        public bool Get(Type key, out IHeroStatusEffectHandler result);
    }
}