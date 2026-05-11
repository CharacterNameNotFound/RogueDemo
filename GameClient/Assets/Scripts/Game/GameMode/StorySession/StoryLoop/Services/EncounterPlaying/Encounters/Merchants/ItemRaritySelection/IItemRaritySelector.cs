using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.ItemRaritySelection
{
    public interface IItemRaritySelector
    {
        public List<ItemRarity> GetRarities(int count, GameBoardModel gameBoardModel);
    }
}