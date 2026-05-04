namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations
{
    public class ItemChargedTrigger : TriggerToken
    {
        public int Index;
        public int Owner;

        public ItemChargedTrigger(int index, int owner)
        {
            Index = index;
            Owner = owner;
        }
    }
}