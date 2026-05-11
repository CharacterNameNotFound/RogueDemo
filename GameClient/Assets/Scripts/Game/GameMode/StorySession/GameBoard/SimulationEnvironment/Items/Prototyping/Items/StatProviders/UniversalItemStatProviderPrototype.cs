using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatProviders;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.StatProviders
{
    public class UniversalItemStatProviderPrototype : StatProviderPrototype
    {
        public ItemStatType ItemStatType;
        
        public float Multiplier = 1;
        
        public override StatProvider GetStatProvider()
        {
            return new UniversalItemStatProvider(ItemStatType, Multiplier);
        }
    }
}