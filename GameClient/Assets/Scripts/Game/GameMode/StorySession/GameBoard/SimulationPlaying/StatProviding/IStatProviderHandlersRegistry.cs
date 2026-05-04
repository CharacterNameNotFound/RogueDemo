using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.StatProviding
{
    public interface IStatProviderHandlersRegistry
    {
        public bool Get(Type key, out IStatProvidingHandler result);
    }
}