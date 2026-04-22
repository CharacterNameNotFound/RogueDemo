using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemIdCollector : IItemIdCollector
    {
        private IItemRegistry _itemRegistry;
        
        /// <summary>
        /// Must not be executed concurrently with other operation over HashSet as it getting changed 
        /// </summary>
        public async UniTask AppendItemHierarchy(Item item, HashSet<string> itemIds, CancellationToken cancellationToken)
        {
            bool isItemAdded = itemIds.Add(item.ItemId);
            
            // if hashset contains list, then there is no need to search and append other items in same tree, as they were already collected
            if (!isItemAdded)
            {
                return;
            }

            Item tempItem = item;
            
            // going upward through hierarchy
            while (tempItem.UpgradedItemId is not null)
            {
                RequestResult<Item> itemLoad = await _itemRegistry.GetOrLoadById(tempItem.UpgradedItemId, cancellationToken);

                if (itemLoad.IsFailure())
                {
                    throw itemLoad.Exception;
                }

                tempItem = itemLoad.GetValue(); 
                
                itemIds.Add(tempItem.ItemId);
            }
            
            // going downward through hierarchy
            while (tempItem.DowngradedItemId is not null)
            {
                RequestResult<Item> itemLoad = await _itemRegistry.GetOrLoadById(tempItem.DowngradedItemId, cancellationToken);

                if (itemLoad.IsFailure())
                {
                    throw itemLoad.Exception;
                }

                tempItem = itemLoad.GetValue(); 
                
                itemIds.Add(tempItem.ItemId);
            }
            
        }
    }
}