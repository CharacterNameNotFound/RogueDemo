using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils.FlyingParticle
{
    public class FlyingParticleConfigs : ScriptableObject
    {
        [field: SerializeField] public Color HasteColor { get; set; }
        [field: SerializeField] public Color SlowColor { get; set; }

        [field: SerializeField] public float FlyingParticlesDuration { get; private set; }
        

        
    }
}