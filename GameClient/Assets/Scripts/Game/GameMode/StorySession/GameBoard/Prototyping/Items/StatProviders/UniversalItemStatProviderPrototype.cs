using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.StatProviders;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.StatProviders
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