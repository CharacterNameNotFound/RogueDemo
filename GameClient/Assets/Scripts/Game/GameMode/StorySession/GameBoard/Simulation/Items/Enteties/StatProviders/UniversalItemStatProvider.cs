using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.StatProviders
{
    public class UniversalItemStatProvider : StatProvider
    {
        private ItemStatType _itemStatType;

        public float Multiplier;
        
        public UniversalItemStatProvider(ItemStatType itemStatType, float multiplier)
        {
            _itemStatType = itemStatType;
            Multiplier = multiplier;
        }
    }
}