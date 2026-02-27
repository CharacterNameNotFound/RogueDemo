using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.StatProviders
{
    public class DamageStatProviderPrototype : StatProviderPrototype
    {
        public override StatProvider GetStatProvider()
        {
            return new StatProvider();
        }
    }
}