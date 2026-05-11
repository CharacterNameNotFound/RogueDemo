using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors
{
    public class ApplyBurnEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider FireStatProvider;
        public StatSet.StatSetComponent ApplicationType;

        public ApplyBurnEffector(TargetSelector targetSelector, StatProvider fireStatProvider, bool isCritAvailable, StatSet.StatSetComponent applicationType)
        {
            TargetSelector = targetSelector;
            FireStatProvider = fireStatProvider;
            ApplicationType = applicationType;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new ApplyBurnEffector(TargetSelector.GetCopy(), FireStatProvider.GetCopy(), IsCritAvailable, ApplicationType);
        }

        public override void AppendDescription(int depth, Item item, StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = FireStatProvider.GetValue(item, itemStatGetter);

            string line = localizationManager.GetLocalized(itemLocalizationConfigs.ApplyBurn, value);
            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{line} {targetLine}");
        }
    }
}