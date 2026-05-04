using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Builders
{
    // ToDo: implement pooling
    public static class TriggerBuilder
    {
        public static ItemCooldownTrigger ItemCooldownTrigger(int index, int owner)
        {
            return new ItemCooldownTrigger(index, owner);
        }
    }
}