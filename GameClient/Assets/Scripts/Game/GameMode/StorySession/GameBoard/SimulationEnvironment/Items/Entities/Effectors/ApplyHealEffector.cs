using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors
{
    public class ApplyHealEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider HealStatProvider;

        public ApplyHealEffector(TargetSelector targetSelector, StatProvider healStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            HealStatProvider = healStatProvider;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new ApplyHealEffector(TargetSelector.GetCopy(), HealStatProvider.GetCopy(), IsCritAvailable);
        }

        public override void AppendDescription(int depth, Item item, StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = HealStatProvider.GetValue(item, itemStatGetter);

            string line = localizationManager.GetLocalized(itemLocalizationConfigs.ApplyHeal, value);
            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{line} {targetLine}");
        }
    }
}