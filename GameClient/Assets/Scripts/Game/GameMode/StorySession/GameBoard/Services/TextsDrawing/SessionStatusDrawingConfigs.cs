using GameWideSystems.LocalizationWrapper;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.TextsDrawing
{
    public class SessionStatusDrawingConfigs : ScriptableObject
    {
        [field: SerializeField] public LocalizedLineKey CyclesText { get; private set; }
        [field: SerializeField] public LocalizedLineKey PlayerStatusText { get; private set; }
    }
}