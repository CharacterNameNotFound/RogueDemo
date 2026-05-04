using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling
{
    public interface IEffectorHandlersRegistry
    {
        public bool Get(Type key, out IEffectorHandler result);
    }
}