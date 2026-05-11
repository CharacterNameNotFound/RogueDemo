using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatProviders
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