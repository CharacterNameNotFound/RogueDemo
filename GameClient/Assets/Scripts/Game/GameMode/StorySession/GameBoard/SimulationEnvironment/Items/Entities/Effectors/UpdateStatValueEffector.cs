using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Effectors
{
    public class UpdateStatValueEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider StatProvider;
        public ItemStatType ItemStatType;
        public StatSet.StatSetComponent ApplicationType;

        public UpdateStatValueEffector(TargetSelector targetSelector, StatProvider statProvider, ItemStatType itemStatType, bool isCritAvailable, StatSet.StatSetComponent applicationType)
        {
            TargetSelector = targetSelector;
            StatProvider = statProvider;
            ItemStatType = itemStatType;
            ApplicationType = applicationType;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new UpdateStatValueEffector(TargetSelector.GetCopy(), StatProvider.GetCopy(), ItemStatType, IsCritAvailable, ApplicationType);
        }

        public override void AppendDescription(
            int depth, 
            Item item, 
            StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = StatProvider.GetValue(item, itemStatGetter);

            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);

            string statTypeLine = localizationManager.GetLocalized(itemLocalizationConfigs.ItemStatTypeToLocalizedLineKey(ItemStatType));

            string valueString = $"{(value >= 0 ? "+" : "-")}{value}";
            
            string line = localizationManager.GetLocalized(
                itemLocalizationConfigs.UpdateStatValue, 
                valueString, 
                statTypeLine, 
                targetLine);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine(line);
        }
        
    }
}