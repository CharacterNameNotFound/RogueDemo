using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Builders;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data
{
    public class ItemCache
    {
        public Item Item;
        
        // Used to find item inside of view, can be changed for ItemContainerComponent, but that will save around x * O(n),
        // which is not a lot as both x (number of passes per frame) and n (number of items) is relevantly low
        public int Index;

        // 0 is a player, 1 is an encounter
        // Preferably to use OwnerIndex enum for readability instead of int literal
        public int OwnerIndex;

        public ItemCache(Item item, int index, int ownerIndex)
        {
            Item = item;
            Index = index;
            OwnerIndex = ownerIndex;
        }

        public ItemChargedTriggerToken ToItemCooldownTrigger()
        {
            return TriggerTokenBuilder.ItemCooldownTrigger(Index, OwnerIndex);
        }
    }
}