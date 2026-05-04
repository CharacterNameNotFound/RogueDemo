using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.ItemRaritySelection
{
    public class ItemRaritySelector : IItemRaritySelector
    {
        private IRNGManager _rngManager;

        public ItemRaritySelector(IRNGManager rngManager)
        {
            _rngManager = rngManager;
        }

        public List<ItemRarity> GetRarities(int count, GameBoardModel gameBoardModel)
        {
            List<ItemRarity> rarities = new(count);

            IRNGProvider randomProvider = _rngManager.GetRandomProvider(RNGGroup.Encounter);

            for (int i = 0; i < count; i++)
            {
                int index = randomProvider.RangeSafe(0, gameBoardModel.PlayerStats.Karma);
                
                rarities.Add((ItemRarity) index);
            }

            return rarities;
        }
    }
}