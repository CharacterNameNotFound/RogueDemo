using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.StatProviders;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.StatProviders
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