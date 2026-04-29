using System.Text;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Localization;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure
{
    public abstract class Trigger
    {
        public abstract Trigger GetCopy();

        public abstract void AppendDescription(int depth,
            Item item,
            StringBuilder itemDescription,
            IItemStatGetter itemStatGetter,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs);
    }
}