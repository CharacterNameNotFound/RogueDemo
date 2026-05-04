using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Builders
{
    public class BattleCacheBuilder : IBattleCacheBuilder
    {
        public BattleCache BattleCache(IGameBoardModelHolder gameBoardModelHolder)
        {
            ItemBoardModel playerBoard = gameBoardModelHolder.GameBoardModel.PlayerBoard;
            ItemBoardModel encounterBoard = gameBoardModelHolder.GameBoardModel.EncounterBoard;
            
            List<ItemCache> playerItems = ReadItemsIntoCache(playerBoard.Items, (int) OwnerIndex.Player);
            List<ItemCache> encounterItems = ReadItemsIntoCache(encounterBoard.Items, (int) OwnerIndex.Encounter);

            BattleSideCache playerSide = new BattleSideCache(playerItems, gameBoardModelHolder.GameBoardModel.PlayerHeroStats, gameBoardModelHolder.GameBoardModel.PlayerBoard);
            BattleSideCache encounterSide = new BattleSideCache(encounterItems, gameBoardModelHolder.GameBoardModel.EncounterHeroStats, gameBoardModelHolder.GameBoardModel.EncounterBoard);

            return new BattleCache(playerSide, encounterSide);
        }
        
        private List<ItemCache> ReadItemsIntoCache(Item[] itemLine, int itemOwner)
        {
            List<ItemCache> result = new List<ItemCache>();
            
            for (int i = 0; i < itemLine.Length;)
            {
                if (itemLine[i] is null)
                {
                    i++;
                    continue;
                }

                ItemCache item = new ItemCache(itemLine[i], i, itemOwner);
                result.Add(item);

                i += itemLine[i].ItemSize;
            }

            return result;
        }
        
    }
}