using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets
{
    [Serializable]
    public enum ItemStatType
    {
        Value,
        Damage,
        Shield,
        Heal,
        Burn,
        Poison,
        CriticalChance,
        CriticalPower,
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