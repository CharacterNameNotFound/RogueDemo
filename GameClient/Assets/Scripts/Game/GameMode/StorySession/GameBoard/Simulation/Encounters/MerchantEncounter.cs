using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Encounters
{
    public abstract class MerchantEncounter : Encounter
    {
        public enum CountSelectorType
        {
            ItemCount,
            BoardSlotCount
        }
        
        [field: Space(20)]
        [field: Header("Merchant configs: ")]
        [field: SerializeField] public CountSelectorType CountSelector { get; private set; }
        [field: SerializeField] public int ItemCount { get; private set; }
        [field: SerializeField] public int BoardSlotCount { get; private set; }
        

        public override EncounterType EncounterType => EncounterType.Merchant;

        protected abstract Item GetItems();


    }
}