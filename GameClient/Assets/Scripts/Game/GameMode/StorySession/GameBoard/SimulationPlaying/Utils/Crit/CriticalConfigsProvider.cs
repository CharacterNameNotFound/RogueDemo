using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils.Crit
{
    public class CriticalConfigsProvider : ScriptableObject, ICriticalConfigsProvider
    {
        [field: SerializeField] public float BasicCripMultiplier { get; private set; } = 2;
    }
}