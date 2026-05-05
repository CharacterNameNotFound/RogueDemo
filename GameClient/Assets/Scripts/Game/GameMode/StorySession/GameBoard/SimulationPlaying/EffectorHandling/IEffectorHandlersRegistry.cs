using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.EffectorHandling
{
    public interface IEffectorHandlersRegistry
    {
        /// <param name="key"> Effector type </param>
        public bool Get(Type key, out IEffectorHandler result);
    }
}