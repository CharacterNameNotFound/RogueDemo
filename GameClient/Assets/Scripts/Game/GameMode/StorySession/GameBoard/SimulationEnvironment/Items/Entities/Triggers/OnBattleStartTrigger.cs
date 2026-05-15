using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Localization;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Triggers
{
    public class OnBattleStartTrigger : Trigger
    {
        public List<Effector> Effectors;
        
        public OnBattleStartTrigger(List<Effector> effectors)
        {
            Effectors = effectors;
        }

        public override Trigger GetCopy()
        {
            return new OnBattleStartTrigger(Effectors.Select(item => item.GetCopy()).ToList());
        }

        public override void AppendDescription(int depth,
            Item item,
            StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs)
        {
            string triggerText = localizationManager.GetLocalized(itemLocalizationConfigs.OnFightStartTrigger);

            itemDescription.Append($"<margin-left={itemLocalizationConfigs.MarginSize * depth}>");
            itemDescription.AppendLine($"{triggerText}:");

            foreach (Effector effector in Effectors)
            {
                effector.AppendDescription(depth + 1, item, itemDescription, itemStatGetter, localizationManager, itemLocalizationConfigs);
            }
            
        }
    }
}