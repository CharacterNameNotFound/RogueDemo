using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors
{
    public class AllOwnerItemsTargetSelector : TargetSelector
    {
        public bool SelectNonCooldownItems;

        public AllOwnerItemsTargetSelector(bool selectNonCooldownItems)
        {
            SelectNonCooldownItems = selectNonCooldownItems;
        }

        public override TargetSelector GetCopy()
        {
            return new AllOwnerItemsTargetSelector(SelectNonCooldownItems);
        }

        public override string GetDescription(Item item,
            IItemStatGetter statGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            return localizationManager.GetLocalized(itemLocalizationConfigs.TargetSelfItemsAll);
        }

    }
}