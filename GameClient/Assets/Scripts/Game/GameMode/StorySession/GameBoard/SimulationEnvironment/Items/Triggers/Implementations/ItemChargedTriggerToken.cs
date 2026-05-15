namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations
{
    public class ItemChargedTriggerToken : TriggerToken
    {
        public int Index;
        public int Owner;

        public ItemChargedTriggerToken(int index, int owner)
        {
            Index = index;
            Owner = owner;
        }
    }
}