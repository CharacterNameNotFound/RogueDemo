using GameWideSystems.LocalizationWrapper;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects
{
    public class EffectorFlyingTextConfigs : ScriptableObject
    {
        [field: SerializeField] public Vector3 FlightTrajectory { get; private set; } = new(0, 4, 0);
        [field: SerializeField] public LocalizedLineKey DealDamage { get; private set; } = new("deal_damage", TranslationCategory.Battle);
        [field: SerializeField] public LocalizedLineKey ApplyBurn { get; private set; } = new("apply_burn", TranslationCategory.Battle);
        [field: SerializeField] public LocalizedLineKey ApplyHealing { get; private set; } = new("apply_healing", TranslationCategory.Battle);
        [field: SerializeField] public LocalizedLineKey ApplyPoison { get; private set; } = new("apply_poison", TranslationCategory.Battle);
        [field: SerializeField] public LocalizedLineKey ApplyRegeneration { get; private set; } = new("apply_regeneration", TranslationCategory.Battle);
        [field: SerializeField] public LocalizedLineKey ApplyShield { get; private set; } = new("apply_shield", TranslationCategory.Battle);
    }
}