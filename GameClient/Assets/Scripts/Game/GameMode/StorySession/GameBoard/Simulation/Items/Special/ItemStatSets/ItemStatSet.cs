using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets
{
    // For now let explicitly define all stats, CPU -> Memory
    [Serializable]
    public class ItemStatSet
    {
        public ItemStatEntry Damage;
        public ItemStatEntry Shield;
        public ItemStatEntry Heal;

        /// <summary>
        /// Used for Cooldown and charge items
        /// </summary>
        public ItemStatEntry Charge;
        
        /// <summary>
        /// Should automatically charge
        /// </summary>
        public bool IsCooldownItem;
        public float CurrentCharge;
        
        /// <summary>
        /// contains 
        /// </summary>
        public ItemStatEntry ChargeModifiers;
    }
}