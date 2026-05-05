using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding
{
    public interface IStatProviderHandlersRegistry
    {
        /// <param name="key"> StatProvider type </param>
        public bool Get(Type key, out IStatProvidingHandler result);
    }
}