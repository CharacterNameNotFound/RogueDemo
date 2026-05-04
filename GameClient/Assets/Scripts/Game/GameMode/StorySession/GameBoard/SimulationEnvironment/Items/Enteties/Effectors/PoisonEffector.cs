using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Effectors
{
    public class PoisonEffector : Effector
    {
        public TargetSelector TargetSelector;
        public StatProvider PoisonStatProvider;

        public PoisonEffector(TargetSelector targetSelector, StatProvider poisonStatProvider, bool isCritAvailable)
        {
            TargetSelector = targetSelector;
            PoisonStatProvider = poisonStatProvider;
            IsCritAvailable = isCritAvailable;
        }

        public override Effector GetCopy()
        {
            return new PoisonEffector(TargetSelector.GetCopy(), PoisonStatProvider.GetCopy(), IsCritAvailable);
        }

        public override void AppendDescription(int depth, Item item, StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            float value = PoisonStatProvider.GetValue(item, itemStatGetter);

            string line = localizationManager.GetLocalized(itemLocalizationConfigs.ApplyPoison, value);
            string targetLine = TargetSelector.GetDescription(item, itemStatGetter, localizationManager, itemLocalizationConfigs);
            
            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{line} {targetLine}");
        }
    }
}