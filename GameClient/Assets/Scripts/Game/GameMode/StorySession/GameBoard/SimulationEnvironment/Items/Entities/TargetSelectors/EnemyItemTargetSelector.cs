using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.TargetSelectors
{
    public class EnemyItemTargetSelector : TargetSelector
    {
        public StatProvider TargetCount;
        public bool SelectNonCooldownItems;

        public EnemyItemTargetSelector(StatProvider targetCount, bool selectNonCooldownItems)
        {
            TargetCount = targetCount;
            SelectNonCooldownItems = selectNonCooldownItems;
        }

        public override TargetSelector GetCopy()
        {
            return new EnemyItemTargetSelector(TargetCount.GetCopy(), SelectNonCooldownItems);
        }

        public override string GetDescription(Item item, 
            IItemStatGetter statGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float count = TargetCount.GetValue(item, statGetter);
            return localizationManager.GetLocalized(itemLocalizationConfigs.TargetOpponentItems, count);
        }

    }
}