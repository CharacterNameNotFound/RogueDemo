using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatProviders
{
    public class UniversalItemStatProvider : StatProvider
    {
        public ItemStatType ItemStatType;
        public float Multiplier;
        
        public UniversalItemStatProvider(ItemStatType itemStatType, float multiplier)
        {
            ItemStatType = itemStatType;
            Multiplier = multiplier;
        }

        public override StatProvider GetCopy()
        {
            return new UniversalItemStatProvider(ItemStatType, Multiplier);
        }

        public override float GetValue(Item item, IItemStatGetter statGetter)
        {
            return statGetter.GetStatValue(item, ItemStatType) * Multiplier;
        }
        
    }
}