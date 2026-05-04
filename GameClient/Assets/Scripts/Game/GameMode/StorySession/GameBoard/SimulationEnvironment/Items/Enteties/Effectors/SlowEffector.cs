using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors
{
    public class SlowEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider SlowDurationProvider;
        
        public SlowEffector(TargetSelector targetSelector, StatProvider slowDurationProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            SlowDurationProvider = slowDurationProvider;
            IsCritAvailable = isCritAvailable;
        }
        
        public override Effector GetCopy()
        {
            return new SlowEffector(TargetSelector.GetCopy(), SlowDurationProvider.GetCopy(), IsCritAvailable);
        }

        public override void AppendDescription(int depth, Item item, StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = SlowDurationProvider.GetValue(item, itemStatGetter);

            string line = localizationManager.GetLocalized(itemLocalizationConfigs.ApplySlow, value);
            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{line} {targetLine}");
        }
    }
}