using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.StatProviders
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
            return statGetter.GetStatValue(item, ItemStatType, StatSet.StatSetComponent.Special, StatSet.StatSetComponent.Special) * Multiplier;
        }
        
    }
}