using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.Utilities.WorldInteractables.Awaiters;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters
{
    public abstract class MerchantEncounter : Encounter
    {
        public enum CountSelectorType
        {
            ItemCount,
            BoardSlotCount
        }

        [field: Space(20)]
        [field: Header("Merchant configs: ")]
        [field: SerializeField] public AssetReferenceSprite MerchantPortrait { get; private set; }
        [field: SerializeField] public CountSelectorType CountSelector { get; private set; }
        [field: SerializeField] public int ItemCount { get; private set; }
        [field: SerializeField] public int BoardSlotCount { get; private set; }

        private GameBoardHolder _gameBoardHolder;
        private IMerchantEncounterRoutines _merchantEncounterRoutines;

        
        
        public override EncounterType EncounterType => EncounterType.Merchant;
        
        public abstract UniTask<IEnumerable<string>> GetItemList(IStoryContext storyContext, CancellationToken cancellationToken);

        [Inject]
        private void Construct(GameBoardHolder gameBoardHolder, IMerchantEncounterRoutines merchantEncounterRoutines)
        {
            _gameBoardHolder = gameBoardHolder;
            _merchantEncounterRoutines = merchantEncounterRoutines;
        }
        
        
        public override async UniTask Play(IStoryContext storyContext, CancellationToken cancellationToken)
        {
            await _merchantEncounterRoutines.ShowElements(this, storyContext, cancellationToken);

            IEnumerable<string> items = await GetItemList(storyContext, cancellationToken);
            await _merchantEncounterRoutines.ShowWares(items, storyContext, cancellationToken);
            
            await InteractablePressWaiter.WaitForPress(
                _gameBoardHolder.GameBoardComponent.GameBoardInteractables.EventEncounterScreenButton, 
                cancellationToken);

            await _merchantEncounterRoutines.HideAll(cancellationToken);

        }
        
    }
}