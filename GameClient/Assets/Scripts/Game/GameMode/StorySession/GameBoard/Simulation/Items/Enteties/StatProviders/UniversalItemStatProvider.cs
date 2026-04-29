using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

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

        public override StatProvider GetCopy()
        {
            return new UniversalItemStatProvider(_itemStatType, Multiplier);
        }

        public override float GetValue(Item item, IItemStatGetter statGetter)
        {
            return statGetter.GetStatValue(item, _itemStatType, StatSet.StatSetComponent.Special, StatSet.StatSetComponent.Special) * Multiplier;
        }
        
    }
}