using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;

namespace Game.GameMode.StorySession.StoryLoop.Encounters.Merchants.ItemRaritySelection
{
    public interface IItemRaritySelector
    {
        public List<ItemRarity> GetRarities(int count, IStoryContext storyContext);
    }
}