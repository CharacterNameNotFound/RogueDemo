using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Localization;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.TargetSelectors
{
    public class AllOwnerItemsSelector : TargetSelector
    {
        public bool SelectNonCooldownItems;

        public AllOwnerItemsSelector(bool selectNonCooldownItems)
        {
            SelectNonCooldownItems = selectNonCooldownItems;
        }

        public override TargetSelector GetCopy()
        {
            return new AllOwnerItemsSelector(SelectNonCooldownItems);
        }

        public override string GetDescription(Item item,
            IItemStatGetter statGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            return localizationManager.GetLocalized(itemLocalizationConfigs.TargetOpponentItemsAll);
        }

    }
}