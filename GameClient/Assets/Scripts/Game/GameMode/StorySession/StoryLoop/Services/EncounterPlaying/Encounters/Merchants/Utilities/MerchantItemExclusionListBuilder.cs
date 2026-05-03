using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Configurations;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Merchants.Utilities
{
    public class MerchantItemExclusionListBuilder : IMerchantItemExclusionListBuilder
    {
        private IItemIdCollector _itemIdCollector;

        public MerchantItemExclusionListBuilder(IItemIdCollector itemIdCollector)
        {
            _itemIdCollector = itemIdCollector;
        }

        public async UniTask<HashSet<string>> BuildIgnoredListIds(GameBoardModel gameBoardModel, CancellationToken cancellationToken)
        {
            HashSet<string> result = new HashSet<string>(ItemConfigurations.ItemCapacity * ItemConfigurations.ItemCommonRarities * 2);

            Item[] playerBoardItems = gameBoardModel.PlayerBoard.Items;
            Item[] stashBoardItems = gameBoardModel.PlayerStashBoard.Items;
            
            await CollectForBoard(playerBoardItems, result, cancellationToken);
            await CollectForBoard(stashBoardItems, result, cancellationToken);
            
            ClearUsedItemsIds(playerBoardItems, result);
            ClearUsedItemsIds(stashBoardItems, result);

            return result;
        }

        private async UniTask CollectForBoard(Item[] boardItems, HashSet<string> result, CancellationToken cancellationToken)
        {
            foreach (Item item in boardItems)
            {
                if (item is null) 
                    continue;
                
                await _itemIdCollector.AppendItemHierarchy(item, result, cancellationToken);
            }
        }

        private void ClearUsedItemsIds(Item[] boardItems, HashSet<string> result)
        {
            foreach (Item item in boardItems)
            {
                if (item is  null)
                    continue;
                
                result.Remove(item.ItemId);
            }
        }
        
    }
}