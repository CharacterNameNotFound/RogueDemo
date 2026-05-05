using System.Text;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatusEffects
{
    public class HasteItemStatusEffect : IItemStatusEffect
    {
        public float Duration;

        public HasteItemStatusEffect(float duration)
        {
            Duration = duration;
        }

        public void Update(float deltaTime)
        {
            Duration -= deltaTime;
        }

        public bool IsReadyToRemove()
        {
            return Duration <= 0;
        }

        public void AppendDescription(
            int depth,
            Item item,
            StringBuilder itemDescription,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            string statusText = localizationManager.GetLocalized(itemLocalizationConfigs.HasteItemStatusEffect, Duration);

            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine(statusText);
        }
        
        public IItemStatusEffect GetCopy()
        {
            return new HasteItemStatusEffect(Duration);
        }
    }
}