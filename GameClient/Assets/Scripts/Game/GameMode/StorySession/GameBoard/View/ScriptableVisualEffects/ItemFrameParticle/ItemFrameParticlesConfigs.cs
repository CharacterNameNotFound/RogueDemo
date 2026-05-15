using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle
{
    // Braking SOLID 
    public class ItemFrameParticlesConfigs : ScriptableObject
    {
        // Registerer configs
        [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
        [field: SerializeField] public int PooledCount { get; private set; }
        
        // Builder configs
        [field: SerializeField] public Color HasteColor { get; private set; }
        [field: SerializeField] public Color SlowColor { get; private set; }
        [field: SerializeField] public Vector2 ShapeSizeAdjustment { get; private set; }
        
    }
}