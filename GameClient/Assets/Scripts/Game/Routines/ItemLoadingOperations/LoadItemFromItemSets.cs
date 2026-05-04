using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.Data.Items;
using Game.GameMode.StorySession.Data.LookUpEntries.Items;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.ManagementSystems.LookUpTableManagement;
using Game.Routines.ItemLoadingOperations.ItemTagToTableEntryConverters;
using Game.Utilities.Constants;
using Newtonsoft.Json;
using SQLite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Result;

namespace Game.Routines.ItemLoadingOperations
{
    public class LoadItemFromItemSets : IItemLookUpTableLoader
    {
        private ILookUpTableManager _lookUpTableManager;
        private IItemTagToTableEntryConverter[] _converters;
        private JsonSerializerSettings _jsonSerializerSettings;

        public LoadItemFromItemSets(
            ILookUpTableManager lookUpTableManager, 
            IItemTagToTableEntryConverter[] converters, 
            JsonSerializerSettings jsonSerializerSettings)
        {
            _lookUpTableManager = lookUpTableManager;
            _converters = converters;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public async UniTask<ProcedureResult> BuildItemLookUp(CancellationToken cancellationToken)
        {
            // collecting all sets
            IList<IResourceLocation> itemSetLocations = await Addressables.LoadResourceLocationsAsync(nameof(AddressableTags.ItemSet));

            CancellationTokenSource localCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            RequestResult<SQLiteAsyncConnection> asyncLookUpDB = _lookUpTableManager.GetAsyncLookUpDB(LookUpTableGroup.Items, cancellationToken);

            if (asyncLookUpDB.IsFailure())
            {
                throw asyncLookUpDB.Exception;
            }
            
            SQLiteAsyncConnection dbConnection = asyncLookUpDB.GetValue();

            localCancellation.Token.Register(() => dbConnection.CloseAsync().AsUniTask().Forget());

            await CreateTables(dbConnection, cancellationToken);

            // We need to avoid transactions, so next best is form packs of inserts limiting amount of calls 
            List<object> inserts = new(100);

            foreach (IResourceLocation location in itemSetLocations)
            {
                await LoadAndRegisterItems(location.PrimaryKey, dbConnection, inserts, localCancellation.Token);
                inserts.Clear();
            }
            
            await dbConnection.CloseAsync();
            localCancellation.Dispose();
            
            return ProcedureResultBuilder.Success();
        }
        
        private async UniTask CreateTables(SQLiteAsyncConnection dbConnection, CancellationToken cancellationToken)
        {
            string namespaceName = typeof(BasicsItemLookUpEntry).Namespace;
            
            IEnumerable<Type> tableTypes = typeof(BasicsItemLookUpEntry)
                .Assembly
                .GetTypes()
                .Where(item => item.Namespace == namespaceName);

            await dbConnection.CreateTablesAsync(types: tableTypes.ToArray())
                .AsUniTask()
                .AttachExternalCancellation(cancellationToken);
        }

        private async UniTask LoadAndRegisterItems(string key, SQLiteAsyncConnection dbConnection, List<object> inserts, CancellationToken cancellationToken)
        {
            ItemSet itemSet = await key.Load<ItemSet>(cancellationToken);

            // GetBytes could be called only from main thread, so we need to read all items and form inserts
            foreach (TextAsset textAsset in itemSet.TextAssets)
            {
                ProcessItemFile(textAsset, inserts);
            }
            
            await dbConnection.InsertAllAsync(inserts);
            
            Addressables.Release(itemSet);
        }

        private void ProcessItemFile(TextAsset textAsset, List<object> inserts)
        {
            Item item = JsonConvert.DeserializeObject<Item>(textAsset.text, _jsonSerializerSettings);

            foreach (IItemTagToTableEntryConverter converter in _converters)
            {
                if (!converter.TryGetInsertObject(item, out object insert))
                {
                    continue;
                }
                
                inserts.Add(insert);
            }
        }
        
        
    }
}