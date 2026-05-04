using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Triggers
{
    public class OnChargedTrigger : Trigger
    {
        public List<Effector> Effectors;
        
        public OnChargedTrigger(List<Effector> effectors)
        {
            Effectors = effectors;
        }

        public override Trigger GetCopy()
        {
            return new OnChargedTrigger(Effectors.Select(item => item.GetCopy()).ToList());
        }

        public override void AppendDescription(int depth,
            Item item,
            StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            string triggerText = localizationManager.GetLocalized(itemLocalizationConfigs.OnChargeTrigger);

            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{triggerText}:");

            foreach (Effector effector in Effectors)
            {
                effector.AppendDescription(depth + 1, item, itemDescription, itemStatGetter, localizationManager, itemLocalizationConfigs);
            }
            
        }
    }
}