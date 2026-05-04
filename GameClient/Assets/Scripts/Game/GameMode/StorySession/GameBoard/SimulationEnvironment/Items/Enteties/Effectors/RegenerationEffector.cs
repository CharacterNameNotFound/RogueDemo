using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors
{
    public class RegenerationEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider RegenerationStatProvider;

        public RegenerationEffector(TargetSelector targetSelector, StatProvider regenerationStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            RegenerationStatProvider = regenerationStatProvider;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new RegenerationEffector(TargetSelector.GetCopy(), RegenerationStatProvider.GetCopy(), IsCritAvailable);
        }

        public override void AppendDescription(int depth, Item item, StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = RegenerationStatProvider.GetValue(item, itemStatGetter);

            string line = localizationManager.GetLocalized(itemLocalizationConfigs.ApplyRegeneration, value);
            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{line} {targetLine}");
        }
    }
}