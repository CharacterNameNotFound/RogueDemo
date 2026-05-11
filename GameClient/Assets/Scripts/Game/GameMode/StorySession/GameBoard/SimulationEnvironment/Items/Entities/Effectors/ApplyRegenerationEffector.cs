using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors
{
    public class ApplyRegenerationEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider RegenerationStatProvider;
        public StatSet.StatSetComponent ApplicationType;


        public ApplyRegenerationEffector(TargetSelector targetSelector, StatProvider regenerationStatProvider, bool isCritAvailable, StatSet.StatSetComponent applicationType)
        {
            TargetSelector = targetSelector;
            RegenerationStatProvider = regenerationStatProvider;
            ApplicationType = applicationType;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new ApplyRegenerationEffector(TargetSelector.GetCopy(), RegenerationStatProvider.GetCopy(), IsCritAvailable, ApplicationType);
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