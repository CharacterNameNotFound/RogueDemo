using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties
{
    [Flags]
    public enum ItemSelectionGroup
    {
        Deck = 1,
        AllCharacters = 1 << 1,
        IncludeNeutral = 1 << 2
    }
}