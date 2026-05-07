using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects
{
    public class StoryVisualEffectManagerConfigs : ScriptableObject
    {
        [Header("Flying text configs")]
        [field: SerializeField] public float FontSize { get; private set; } = 2;
        [field: SerializeField] public float Duration { get; set; } = 2;
        [field: SerializeField] public float FadeAtPercentile { get; set; } = 0.5f;
        [field: SerializeField] public float FadeValue { get; set; } = 0.3f;

    }
}