using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemIdCollector : IItemIdCollector
    {
        private IItemRegistry _itemRegistry;

        public ItemIdCollector(IItemRegistry itemRegistry)
        {
            _itemRegistry = itemRegistry;
        }

        public async UniTask AppendItemHierarchy(string itemId, HashSet<string> itemIds, CancellationToken cancellationToken)
        {
            RequestResult<Item> itemRequest = await _itemRegistry.GetOrLoadById(itemId, cancellationToken);
            
            if (itemRequest.IsFailure())
            {
                throw itemRequest.Exception;
            }
            
            await AppendItemHierarchy(itemRequest.GetValue(), itemIds, cancellationToken);
        }

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