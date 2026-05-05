using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection
{
    public interface ITargetSelectionHandlersRegistry
    {
        /// <param name="key"> TargetSelector type </param>
        public bool Get(Type key, out ITargetSelectionHandler result);
    }
}