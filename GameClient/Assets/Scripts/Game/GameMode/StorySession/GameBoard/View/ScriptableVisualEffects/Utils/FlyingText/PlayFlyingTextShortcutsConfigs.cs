using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils.FlyingText
{
    public class PlayFlyingTextShortcutsConfigs : ScriptableObject
    {
        [field: SerializeField] public float FontSize { get; private set; } = 2f;
        [field: SerializeField] public float FontSizeOnCrit { get; private set; } = 4f;
        [field: SerializeField] public float DispersionRadius { get; private set; } = 2.5f;
    }
}