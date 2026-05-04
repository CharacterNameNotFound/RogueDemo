using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.TargetSelection
{
    public interface ITargetSelectionHandlersRegistry
    {
        public bool Get(Type key, out ITargetSelectionHandler result);
    }
}