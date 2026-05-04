using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets
{
    [Serializable]
    public enum ItemStatType
    {
        Value,
        Damage,
        Shield,
        Heal,
        Fire,
        Poison,
        CriticalChance,
        CriticalDamage,
        UsageCount,
        MaxCharge,
        ChargeSpeed,
        SlowTargetCount,
        SlowDuration,
        HasteTargetCount,
        HasteDuration,
        Regeneration,
    }
}