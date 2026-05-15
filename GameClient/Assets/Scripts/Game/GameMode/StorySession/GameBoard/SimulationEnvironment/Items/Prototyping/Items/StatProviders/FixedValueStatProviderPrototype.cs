using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatProviders;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.StatProviders
{
    public class FixedValueStatProviderPrototype : StatProviderPrototype
    {
        public float Value;
        
        public override StatProvider GetStatProvider()
        {
            return new FixedValueStatProvider(Value);
        }
    }
}