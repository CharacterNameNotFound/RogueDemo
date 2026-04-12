using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemLoader : IItemLoader
    {
        private JsonSerializerSettings _jsonSerializerSettings;
        private IItemLoaderConfigProvider _itemLoaderConfigProvider;

        public ItemLoader(
            JsonSerializerSettings jsonSerializerSettings, 
            IItemLoaderConfigProvider itemLoaderConfigProvider)
        {
            _jsonSerializerSettings = jsonSerializerSettings;
            _itemLoaderConfigProvider = itemLoaderConfigProvider;
        }
        
        public async UniTask<RequestResult<Item>> LoadById(string itemId, CancellationToken cancellationToken)
        {
            string address = $"{_itemLoaderConfigProvider.ItemPrefix}{itemId}";

            TextAsset itemText = await address.Load<TextAsset>(cancellationToken);

            Item item = JsonConvert.DeserializeObject<Item>(itemText.text, _jsonSerializerSettings);

            Addressables.Release(itemText);
            
            return item.AsRequestResult();
        }
        
        
    }
}