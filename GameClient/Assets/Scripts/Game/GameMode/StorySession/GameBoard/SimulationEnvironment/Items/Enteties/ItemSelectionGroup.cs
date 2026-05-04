using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties
{
    [Flags]
    public enum ItemSelectionGroup
    {
        Deck = 1,
        AllCharacters = 1 << 1,
        IncludeNeutral = 1 << 2
    }
}