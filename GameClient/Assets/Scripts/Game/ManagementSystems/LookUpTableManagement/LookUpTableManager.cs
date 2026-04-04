using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using SQLite;
using UnityEngine;
using Utils.UtilityTypes.Result;

namespace Game.ManagementSystems.LookUpTableManagement
{
    public class LookUpTableManager : ILookUpTableManager
    {
        private ILookUpTableConfigProvider _lookUpTableConfigProvider;

        private Dictionary<LookUpTableGroup, string> _openedDBs = new();

        public LookUpTableManager(ILookUpTableConfigProvider lookUpTableConfigProvider)
        {
            _lookUpTableConfigProvider = lookUpTableConfigProvider;
        }

        // for now I will just fill it out and hold open db all the time, as it is core of the game anyway
        public async UniTask<ProcedureResult> LoadLookUpTables(CancellationToken cancellationToken)
        {
            string folderPath = _lookUpTableConfigProvider.GetLookUpTablesFolderPath();
            
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string itemTable = _lookUpTableConfigProvider.GetLookUpTablePath(LookUpTableGroup.Items);

                SQLiteConnection db = new SQLiteConnection(itemTable, 
                    SQLiteOpenFlags.Create 
                        | SQLiteOpenFlags.FullMutex 
                        | SQLiteOpenFlags.ReadWrite);
                
                db.Close();
                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return ProcedureResultBuilder.Success();
        }

        public RequestResult<SQLiteAsyncConnection> GetAsyncLookUpDB(LookUpTableGroup tableGroup, CancellationToken cancellationToken)
        {
            bool isPresent = _openedDBs.TryGetValue(tableGroup, out string value);

            if (!isPresent)
            {
                return RequestResultBuilder.Error<SQLiteAsyncConnection>("failed to find db");
            }

            return new SQLiteAsyncConnection(value).AsRequestResult();
        }

        public RequestResult<SQLiteConnection> GetLookUpDB(LookUpTableGroup tableGroup, CancellationToken cancellationToken)
        {
            bool isPresent = _openedDBs.TryGetValue(tableGroup, out string value);

            if (!isPresent)
            {
                return RequestResultBuilder.Error<SQLiteConnection>("failed to find db");
            }

            return new SQLiteConnection(value).AsRequestResult();
        }
    }
}