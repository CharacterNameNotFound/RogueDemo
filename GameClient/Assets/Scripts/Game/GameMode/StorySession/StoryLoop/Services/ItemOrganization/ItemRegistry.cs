using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using UnityEngine;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemRegistry : IItemRegistry
    {
        private Dictionary<string, Item> _items = new();
        
        private IItemLoader _itemLoader;

        public ItemRegistry(IItemLoader itemLoader)
        {
            _itemLoader = itemLoader;
        }

        public void Initialize(Dictionary<string, Item> items)
        {
            _items = items;
        }

        public bool TryGetById(string id, out Item item)
        {
            return _items.TryGetValue(id, out item);
        }
        
        public async UniTask<RequestResult<Item>> GetOrLoadById(string id, CancellationToken cancellationToken)
        {
            bool isPresentInRegistry = _items.TryGetValue(id, out Item result);

            if (isPresentInRegistry)
            {
                return result.AsRequestResult();
            }

            RequestResult<Item> loadByIdItem = await _itemLoader.LoadById(id, cancellationToken);

            if (loadByIdItem.IsFailure())
            {
                throw loadByIdItem.Exception;
            }

            Item value = loadByIdItem.GetValue();

            _items[value.ItemId] = value;

            return loadByIdItem;
        }

        /// <summary>
        /// This metod should be used with caution, if you want to make multiple parallel calls, use AppendItemsById, to avoid concurecy aroud dictionary
        /// </summary>
        public async UniTask<RequestResult<bool>> AppendItemById(string id, CancellationToken cancellationToken)
        {
            if (_items.ContainsKey(id))
            {
                return false.AsRequestResult();
            }
            
            RequestResult<Item> loadByIdItem = await _itemLoader.LoadById(id, cancellationToken);

            if (loadByIdItem.IsFailure())
            {
                throw loadByIdItem.Exception;
            }

            Item value = loadByIdItem.GetValue();

            _items[value.ItemId] = value;

            return true.AsRequestResult();
        }

        public async UniTask AppendItemsById(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            List<UniTask<RequestResult<Item>>> tasks = new();
            
            foreach (string id in ids)
            {
                if (_items.ContainsKey(id))
                {
                    continue;
                }

                tasks.Add(_itemLoader.LoadById(id, cancellationToken));
            }

            RequestResult<Item>[] loadedItems = await tasks;
            
            foreach (RequestResult<Item> itemResult in loadedItems)
            {
                if (itemResult.IsFailure())
                {
                    Debug.Log(itemResult.Error);
                    continue;
                }

                Item item = itemResult.GetValue();

                _items.Add(item.ItemId, item);
                
            }
            
        }

        public bool AppendItem(Item item)
        {
            return _items.TryAdd(item.ItemId, item);
        }
        

        public void CleanUp()
        {
            
        }
        
    }
}