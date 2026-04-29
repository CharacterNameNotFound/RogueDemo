using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameWideSystems.LocalizationWrapper
{
    public class LocalizationManager : ILocalizationManager
    {

        private Logger.Logger _logger;
        
        private Dictionary<string, StringTable> _localTables;

        public LocalizationManager(Logger.Logger logger)
        {
            _logger = logger;
        }
        
        public async UniTask Initialize()
        {
            AsyncOperationHandle<IList<StringTable>> asyncOperationHandle = LocalizationSettings.StringDatabase.GetAllTables();
            await asyncOperationHandle.ToUniTask();

            IList<StringTable> result = asyncOperationHandle.Result;
            _localTables = result.ToDictionary(table => table.TableCollectionName);
        }

        public string GetLocalized(LocalizedLineKey key)
        {
            TryGetLocalized(key, out var line);
            return line;
        }

        public bool TryGetLocalized(LocalizedLineKey key, out string localizedLine)
        {
            return TryGetLocalized(key.Key, key.Category, out  localizedLine);
        }
        
        public bool TryGetLocalized(string key, TranslationCategory category, out string localizedLine)
        {
            if (!GetTableEntry(key, category, out StringTableEntry enty))
            {
                localizedLine = key;
                return false;
            }

            localizedLine = enty.GetLocalizedString();
            return true;
        }
        
        public bool TryGetLocalized(LocalizedLineKey key, object replacements, out string localizedLine)
        {
            return TryGetLocalized(key.Key, key.Category, replacements, out  localizedLine);
        }

        public bool TryGetLocalized(string key, TranslationCategory category, object replacements, out string localizedLine)
        {
            if (!GetTableEntry(key, category, out StringTableEntry enty))
            {
                localizedLine = key;
                return false;
            }

            localizedLine = enty.GetLocalizedString(replacements);
            return true;
        }

        private bool GetTableEntry(string key, TranslationCategory category, out StringTableEntry tableEnty)
        {
            tableEnty = null;
            if (!_localTables.TryGetValue(category.ToString(), out StringTable table))
            {
                _logger.Log($"Table with name \"{category.ToString()}\" was not loaded");
                return false;
            }

            tableEnty = table.GetEntry(key);
            if (tableEnty is null)
            {
                _logger.Log($"Enty with name \"{key}\" was not present in \"{category.ToString()}\" table");
                return false;
            }

            return true;
        }
        
        
    }
}
