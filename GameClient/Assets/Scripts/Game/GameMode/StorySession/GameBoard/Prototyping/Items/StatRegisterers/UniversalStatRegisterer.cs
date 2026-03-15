using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.StatRegisterers
{
    public class UniversalStatRegisterer : StatRegistererPrototype
    {
        [Serializable]
        public class UniversalStatRegistererEntry
        {
            public ItemStatType ItemStatType;
            public ItemStatEntry ItemStatSet;
        }

        public List<UniversalStatRegistererEntry> StatEntries;
        
        public bool IsCooldownItem;
        public float CurrentCharge;
        
        public override void AppendStats(ItemStatSet itemStats)
        {
            foreach (UniversalStatRegistererEntry entry in StatEntries)
            {
                itemStats.Stats[entry.ItemStatType] = entry.ItemStatSet;
            }

            itemStats.IsCooldownItem = IsCooldownItem;
            itemStats.CurrentCharge = CurrentCharge;

        }
    }
}