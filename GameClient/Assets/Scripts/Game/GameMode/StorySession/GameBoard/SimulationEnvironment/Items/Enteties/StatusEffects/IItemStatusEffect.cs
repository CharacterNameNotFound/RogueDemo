using System.Text;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Localization;
using GameWideSystems.LocalizationWrapper;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.StatusEffects
{
    public interface IItemStatusEffect
    {
        public void Update(float deltaTime);
        public bool IsReadyToRemove(); 
        
        public void AppendDescription(
            int depth,
            Item item,
            StringBuilder itemDescription,
            ILocalizationManager localizationManager,
            ItemDescriptionLocalizationConfigs itemLocalizationConfigs);
        
        public IItemStatusEffect GetCopy();
    }
}