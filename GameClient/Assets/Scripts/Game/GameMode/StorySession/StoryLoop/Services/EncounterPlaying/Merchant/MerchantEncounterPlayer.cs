using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using Game.GameMode.StorySession.Utilities.WorldInteractables.Awaiters;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;
using Zenject;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant
{
    public class MerchantEncounterPlayer : IMerchantEncounterPlayer
    {
        private IItemPresenter _itemPresenter;
        private GameBoardHolder _gameBoardHolder;

        public MerchantEncounterPlayer(
            IItemPresenter itemPresenter, 
            GameBoardHolder gameBoardHolder)
        {
            _itemPresenter = itemPresenter;
            _gameBoardHolder = gameBoardHolder;
        }


        public async UniTask Play(Encounter encounter, IStoryContext storyContext, CancellationToken cancellationToken)
        {
            MerchantEncounter merchantEncounter = (MerchantEncounter)encounter;

            Sprite portrait = await merchantEncounter.MerchantPortrait.Load<Sprite>(cancellationToken);
            _gameBoardHolder.GameBoardComponent.EncounterBoard.EncounterView.Render(portrait);
            _gameBoardHolder.GameBoardComponent.EncounterBoard.SwitchToView(EncounterBoard.BoardType.Encounter);
            
            ProjectContext.Instance.Container.Inject(merchantEncounter);
            
            IEnumerable<string> itemList = await merchantEncounter.GetItemList(storyContext, cancellationToken);
            await _itemPresenter.ShowItems(itemList, cancellationToken);

            await InteractablePressWaiter.WaitForPress(
                _gameBoardHolder.GameBoardComponent.GameBoardInteractables.EventEncounterScreenButton, 
                cancellationToken);
            
            
            _itemPresenter.RemoveEncounterItemsImmediate();
            _gameBoardHolder.GameBoardComponent.EncounterBoard.HideCurrentView();
            Addressables.Release(portrait);
            
        }
        
    }
}