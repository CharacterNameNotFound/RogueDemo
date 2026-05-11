using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors
{
    public class OwnerItemTargetSelector : TargetSelector
    {
        public StatProvider TargetCount;
        public bool SelectNonCooldownItems;

        public OwnerItemTargetSelector(StatProvider targetCount, bool selectNonCooldownItems)
        {
            TargetCount = targetCount;
            SelectNonCooldownItems = selectNonCooldownItems;
        }

        public override TargetSelector GetCopy()
        {
            return new OwnerItemTargetSelector(TargetCount.GetCopy(), SelectNonCooldownItems);
        }

        public override string GetDescription(Item item,
            IItemStatGetter statGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float count = TargetCount.GetValue(item, statGetter);
            return localizationManager.GetLocalized(itemLocalizationConfigs.TargetSelfItems, count);
        }

    }
}