using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public class EncounterRegistry : IEncounterRegistry
    {
        private IEncounterLoader _encounterLoader;

        private Dictionary<string, Encounter> _encounters;

        public EncounterRegistry(IEncounterLoader encounterLoader)
        {
            _encounterLoader = encounterLoader;
        }


        public void Initialize(List<Encounter> encounters)
        {
            _encounters = encounters.ToDictionary(item => item.EncounterId);
        }

        public UniTask InitializeWithIds(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            _encounters = new(ids.Count());
            return AppendEncountersById(ids, cancellationToken);
        }

        public bool TryGetById(string id, out Encounter item)
        {
            return _encounters.TryGetValue(id, out item);
        }

        public async UniTask<RequestResult<Encounter>> GetOrLoadById(string id, CancellationToken cancellationToken)
        {
            bool isPresentInRegistry = _encounters.TryGetValue(id, out Encounter result);

            if (isPresentInRegistry)
            {
                return result.AsRequestResult();
            }

            RequestResult<Encounter> loadByIdItem = await _encounterLoader.LoadById(id, cancellationToken);

            if (loadByIdItem.IsFailure())
            {
                throw loadByIdItem.Exception;
            }

            Encounter value = loadByIdItem.GetValue();

            _encounters[value.EncounterId] = value;

            return loadByIdItem;
        }

        /// <summary>
        /// This method should be used with caution, if you want to make multiple parallel calls, use AppendEncountersById, to avoid concurrency around dictionary
        /// </summary>
        public async UniTask<RequestResult<bool>> AppendEncounterById(string id, CancellationToken cancellationToken)
        {
            if (_encounters.ContainsKey(id))
            {
                return false.AsRequestResult();
            }
            
            RequestResult<Encounter> loadByIdItem = await _encounterLoader.LoadById(id, cancellationToken);

            if (loadByIdItem.IsFailure())
            {
                throw loadByIdItem.Exception;
            }

            Encounter value = loadByIdItem.GetValue();

            _encounters[value.EncounterId] = value;

            return true.AsRequestResult();
        }

        public async UniTask AppendEncountersById(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            List<UniTask<RequestResult<Encounter>>> tasks = new();
            
            foreach (string id in ids)
            {
                if (_encounters.ContainsKey(id))
                {
                    continue;
                }

                tasks.Add(_encounterLoader.LoadById(id, cancellationToken));
            }

            RequestResult<Encounter>[] loadedItems = await tasks;
            
            foreach (RequestResult<Encounter> itemResult in loadedItems)
            {
                if (itemResult.IsFailure())
                {
                    Debug.Log(itemResult.Error);
                    continue;
                }

                Encounter item = itemResult.GetValue();

                _encounters.Add(item.EncounterId, item);
                
            }
        }

        public bool Append(Encounter encounter)
        {
            return _encounters.TryAdd(encounter.EncounterId, encounter);
        }

        public void AppendRange(IEnumerable<Encounter> encounters)
        {
            foreach (Encounter item in encounters)
            {
                _encounters.TryAdd(item.EncounterId, item);
            }
        }

        public List<string> GetAllRegisteredIds()
        {
            return _encounters.Keys.ToList();
        }

        public void CleanUp()
        {
            foreach (Encounter item in _encounters.Values.ToArray())
            {
                Addressables.Release(item);
            }
            
        }
        
    }
}