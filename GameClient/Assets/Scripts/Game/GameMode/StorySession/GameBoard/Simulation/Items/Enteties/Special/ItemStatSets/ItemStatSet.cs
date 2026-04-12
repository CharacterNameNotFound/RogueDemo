using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets
{
    [Serializable]
    public class ItemStatSet
    {
        public Dictionary<ItemStatType, ItemStatEntry> Stats = new();
        
        // accessed through code only
        [HideInInspector] public bool IsCooldownItem;
        [HideInInspector] public float CurrentCharge;
    }
}