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

        public ItemStatSet GetCopy()
        {
            ItemStatSet itemStatSet = new ItemStatSet();

            itemStatSet.IsCooldownItem = IsCooldownItem;
            itemStatSet.CurrentCharge = CurrentCharge;
            
            itemStatSet.Stats = new Dictionary<ItemStatType, ItemStatEntry>();
            foreach (KeyValuePair<ItemStatType, ItemStatEntry> item in Stats)
            {
                itemStatSet.Stats.Add(item.Key, item.Value.GetCopy());
            }

            return itemStatSet;
        }
    }
}