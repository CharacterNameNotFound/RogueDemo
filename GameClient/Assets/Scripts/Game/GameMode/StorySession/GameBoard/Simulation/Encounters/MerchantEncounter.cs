using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Encounters
{
    public abstract class MerchantEncounter : Encounter
    {
        public enum CountSelectorType
        {
            ItemCount,
            BoardSlotCount
        }
        
        [field: SerializeField] public CountSelectorType CountSelector { get; private set; }
        [field: SerializeField] public int ItemCount { get; private set; }
        [field: SerializeField] public int BoardSlotCount { get; private set; }
        

        public override EncounterType EncounterType => EncounterType.Merchant;

        protected abstract 
        
        
    }
}