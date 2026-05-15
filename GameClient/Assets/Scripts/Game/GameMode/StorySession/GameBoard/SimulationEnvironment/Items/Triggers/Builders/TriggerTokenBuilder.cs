using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Implementations;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Triggers.Builders
{
    // ToDo: implement pooling
    public static class TriggerTokenBuilder
    {
        public static ItemChargedTriggerToken ItemCooldownTrigger(int index, int owner)
        {
            return new ItemChargedTriggerToken(index, owner);
        }

        public static BattleStartTriggerToken BattleStartTriggerToken()
        {
            return new BattleStartTriggerToken();
        }
        
    }
}