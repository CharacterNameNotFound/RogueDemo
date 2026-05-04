using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Builders
{
    // ToDo: implement pooling
    public static class TriggerTokenBuilder
    {
        public static ItemChargedTrigger ItemCooldownTrigger(int index, int owner)
        {
            return new ItemChargedTrigger(index, owner);
        }
    }
}