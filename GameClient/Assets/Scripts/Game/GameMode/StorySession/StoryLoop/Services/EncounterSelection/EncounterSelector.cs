using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using GameWideSystems.InputManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Result;
using Logger = GameWideSystems.Logger.Logger;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterSelection
{
    public class EncounterSelector : IEncounterSelector
    {
        private readonly IEncounterSelectorConfigsProvider _encounterSelectorConfigs;
        private GameBoardHolder _gameBoardHolder;
        private StorySessionEncounterSelectionInputLayer _inputLayer;
        private IInputHost _inputHost;
        private IEncounterRegistry _encounterRegistry;
        private Logger _logger;

        private GameObject _holder;
        private List<EncounterSelectorEntryComponent> _views;
        
        public EncounterSelector(
            IEncounterSelectorConfigsProvider encounterSelectorConfigs, 
            GameBoardHolder gameBoardHolder, 
            StorySessionEncounterSelectionInputLayer inputLayer, 
            IInputHost inputHost, 
            IEncounterRegistry encounterRegistry, 
            Logger logger)
        {
            _encounterSelectorConfigs = encounterSelectorConfigs;
            _gameBoardHolder = gameBoardHolder;
            _inputLayer = inputLayer;
            _inputHost = inputHost;
            _encounterRegistry = encounterRegistry;
            _logger = logger;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            _holder = new GameObject("EncounterSelectorHolder");

            List<UniTask<GameObject>> tasks = new List<UniTask<GameObject>>();

            InstantiationParameters instantiationParameters = new InstantiationParameters(_holder.transform, false);

            for (int i = 0; i < _encounterSelectorConfigs.PreparedSelectionInstancesCount; i++)
            {
                UniTask<GameObject> instantiateTask = _encounterSelectorConfigs.EncounterSelectionPrefab.Instantiate(instantiationParameters, cancellationToken);
                tasks.Add(instantiateTask);
            }

            GameObject[] instances = await tasks;

            _views = new List<EncounterSelectorEntryComponent>(instances.Select(item => item.GetComponent<EncounterSelectorEntryComponent>()));
            foreach (EncounterSelectorEntryComponent item in _views)
            {
                item.gameObject.SetActive(false);
            }
            
            
            _inputHost.AddInputLayer(_inputLayer);
            _inputLayer.SetActive(false);
        }

        public async UniTask<int> StartEncounterSelection(List<string> encounterIds, CancellationToken cancellationToken)
        {
            _inputLayer.SetActive(true);

            // making sure that there is no items with selected == true
            foreach (EncounterSelectorEntryComponent item in _views)
            {
                item.IsSelected = false;
            }
            
            ItemLineComponent encounterItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine;
            
            float holderSizeX = encounterItemLine.SpriteRenderer.bounds.size.x - _encounterSelectorConfigs.SideFreeSpace;
            float itemSizeX = _views[0].SpriteRenderer.size.x;
            float spawnSpaceX = holderSizeX - itemSizeX; // horizontal length that could be used as X coordinate for object

            // extend to first/last possible X coordinate for spawn
            float startExtendX = encounterItemLine.transform.position.x - spawnSpaceX / 2;
            
            float stepX = encounterIds.Count > 0 ? 
                spawnSpaceX / (encounterIds.Count - 1) : 
                0;

            List<UniTask<Sprite>> spriteTasks = new List<UniTask<Sprite>>();
            
            // Showing encounters
            for(int i = 0; i < encounterIds.Count; i++)
            {
                RequestResult<Encounter> requestResult = await _encounterRegistry.GetOrLoadById(encounterIds[i], cancellationToken);

                if (requestResult.IsFailure())
                {
                     throw requestResult.Exception;
                }

                Encounter encounter = requestResult.GetValue();

                // setting object position
                _views[i].transform.SetParent(encounterItemLine.transform);
                _views[i].transform.localPosition = new Vector3(startExtendX + stepX * i, 0, 0);
                _views[i].Encounter = encounter;
                _views[i].ItemId = i;

                spriteTasks.Add(encounter.Portrait.Load<Sprite>(cancellationToken));
            }

            Sprite[] sprites = await spriteTasks;
            
            for (int i = 0; i < encounterIds.Count; i++)
            {
                _views[i].SpriteRenderer.sprite = sprites[i];
                _views[i].gameObject.SetActive(true);
            }

            // waiting for at least one to get selected
            await UniTask.WaitUntil(() => _views.Any(item => item.IsSelected), cancellationToken:cancellationToken);

            _inputLayer.SetActive(false);
            
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            
            // resetting assets
            for (int i = 0; i < encounterIds.Count; i++)
            {
                _views[i].transform.SetParent(_holder.transform);
                _views[i].gameObject.SetActive(false);
                _views[i].SpriteRenderer.sprite = null;
                Addressables.Release(sprites[i]);
            }

            return _views.First(item => item.IsSelected).ItemId;
        }

        public void CleanUp(CancellationToken cancellationToken)
        {
            foreach (EncounterSelectorEntryComponent item in _views)
            {
                Addressables.ReleaseInstance(item.gameObject);
            }

            _views = null;
            _inputHost.RemoveInputLayer(_inputLayer);
            _inputLayer.SetActive(false);
        }
        
        
        
        
    }
}