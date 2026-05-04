using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations
{
    public class ItemCooldownTrigger : Trigger
    {
        public int Index;
        public int Owner;

        public ItemCooldownTrigger(int index, int owner)
        {
            Index = index;
            Owner = owner;
        }
    }
}