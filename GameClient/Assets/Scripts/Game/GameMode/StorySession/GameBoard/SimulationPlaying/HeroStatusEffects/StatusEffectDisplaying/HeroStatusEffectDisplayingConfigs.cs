using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectDisplaying
{
    public class HeroStatusEffectDisplayingConfigs : ScriptableObject
    {
        [field: SerializeField] public int PreSpawnedCount { get; private set; } = 40;
        [field: SerializeField] public AssetReferenceGameObject StatusEffectIcon { get; private set; }
        
        [Header("Icons")]
        [field: SerializeField] public AssetReferenceSprite RegenerationIcon { get; private set; }
        [field: SerializeField] public AssetReferenceSprite BurnIcon { get; private set; }
        [field: SerializeField] public AssetReferenceSprite PoisonIcon { get; private set; }
        

    }
}