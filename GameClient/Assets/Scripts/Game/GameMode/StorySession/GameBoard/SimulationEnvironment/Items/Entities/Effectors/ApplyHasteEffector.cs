using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors
{
    public class ApplyHasteEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider HasteDurationProvider;

        public ApplyHasteEffector(
            TargetSelector targetSelector, 
            StatProvider hasteDurationProvider, 
            bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            HasteDurationProvider = hasteDurationProvider;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new ApplyHasteEffector(TargetSelector.GetCopy(), HasteDurationProvider.GetCopy(), IsCritAvailable);
        }

        public override void AppendDescription(int depth, Item item, StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = HasteDurationProvider.GetValue(item, itemStatGetter);

            string line = localizationManager.GetLocalized(itemLocalizationConfigs.ApplyHaste, value);
            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{line} {targetLine}");
        }
    }
}