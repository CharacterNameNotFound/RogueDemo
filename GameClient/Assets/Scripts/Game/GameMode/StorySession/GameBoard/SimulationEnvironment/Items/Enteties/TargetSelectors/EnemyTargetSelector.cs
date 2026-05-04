using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.TargetSelectors
{
    public class EnemyTargetSelector : TargetSelector
    {
        public override TargetSelector GetCopy()
        {
            return new EnemyTargetSelector();
        }
        
        public override string GetDescription(Item item,
            IItemStatGetter statGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            return localizationManager.GetLocalized(itemLocalizationConfigs.TargetOpponent);
        }

    }
}