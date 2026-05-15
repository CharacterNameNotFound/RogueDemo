using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.Services.PlayerStatusUpdating;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Gifts.Routines;
using Game.GameMode.StorySession.Utilities.WorldInteractables.Awaiters;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Gifts
{
    public abstract class GiftEncounter : Encounter
    {
        [field: Space(20)]
        [field: Header("Gift configs: ")]
        [field: SerializeField] public AssetReferenceSprite GiftEncounterPortrait { get; private set; }
        
        private GameBoardHolder _gameBoardHolder;
        private IGiftEncounterRoutines _giftEncounterRoutines;
        private IItemStatGetter _itemStatGetter;
        private IPlayerStatusUpdater _statusUpdater;
        private IGameBoardModelHolder _gameBoardModelHolder;
        
        public override EncounterType EncounterType => EncounterType.Gift;

        
        public abstract UniTask<IEnumerable<string>> GetItemList(GameBoardModel gameBoardModel, CancellationToken cancellationToken);
        
        [Inject]
        private void Construct(
            GameBoardHolder gameBoardHolder, 
            IItemStatGetter itemStatGetter,
            IPlayerStatusUpdater statusUpdater,
            IGameBoardModelHolder gameBoardModelHolder,
            IGiftEncounterRoutines giftEncounterRoutines)
        {
            _gameBoardHolder = gameBoardHolder;
            _itemStatGetter = itemStatGetter;
            _statusUpdater = statusUpdater;
            _gameBoardModelHolder = gameBoardModelHolder;
            _giftEncounterRoutines = giftEncounterRoutines;
        }
        
        
        public override async UniTask Play(GameBoardModel gameBoardModel, CancellationToken cancellationToken)
        {
            IEnumerable<string> items = await GetItemList(gameBoardModel, cancellationToken);
            
            
            await _giftEncounterRoutines.ShowElements(this, cancellationToken);
            await _giftEncounterRoutines.ShowWares(items, cancellationToken);
            
            
            await InteractablePressWaiter.WaitForPress(
                _gameBoardHolder.GameBoardComponent.GameBoardInteractables.EventEncounterScreenButton, 
                cancellationToken);
            
            
            await _giftEncounterRoutines.HideAll(cancellationToken);
        }
        
        
        
        
    }
}