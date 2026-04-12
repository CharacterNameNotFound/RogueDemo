using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.StatRegisterers
{
    public class UniversalStatRegisterer : StatRegistererPrototype
    {
        [Serializable]
        public class UniversalStatRegistererEntry
        {
            public ItemStatType ItemStatType;
            public ItemStatEntry ItemStatSet = new(new StatSet(), new StatSet(1,1,1,1,1));
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