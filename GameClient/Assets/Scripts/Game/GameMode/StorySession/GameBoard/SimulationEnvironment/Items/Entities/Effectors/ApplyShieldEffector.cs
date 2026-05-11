using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors
{
    public class ApplyShieldEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider ShieldStatProvider;

        public ApplyShieldEffector(TargetSelector targetSelector, StatProvider shieldStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            ShieldStatProvider = shieldStatProvider;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new ApplyShieldEffector(TargetSelector.GetCopy(), ShieldStatProvider.GetCopy(), IsCritAvailable);
        }

        public override void AppendDescription(int depth, Item item, StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = ShieldStatProvider.GetValue(item, itemStatGetter);

            string line = localizationManager.GetLocalized(itemLocalizationConfigs.ApplyShield, value);
            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{line} {targetLine}");
        }
    }
}