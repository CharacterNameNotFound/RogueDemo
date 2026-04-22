using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.StoryLoop.Encounters.Merchants.ItemRaritySelection
{
    public class ItemRaritySelector : IItemRaritySelector
    {
        private IRNGManager _rngManager;

        public ItemRaritySelector(IRNGManager rngManager)
        {
            _rngManager = rngManager;
        }

        public List<ItemRarity> GetRarities(int count, IStoryContext storyContext)
        {
            List<ItemRarity> rarities = new(count);

            IRNGProvider randomProvider = _rngManager.GetRandomProvider(RNGGroup.Encounter);

            for (int i = 0; i < count; i++)
            {
                int index = randomProvider.RangeSafe(0, storyContext.GameBoardModel.PlayerStats.Karma);
                
                rarities.Add((ItemRarity) index);
            }

            return rarities;
        }
    }
}