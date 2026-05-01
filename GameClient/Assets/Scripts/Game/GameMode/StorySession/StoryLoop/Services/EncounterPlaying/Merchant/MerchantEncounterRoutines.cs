using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using UnityEngine;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant
{
    public class MerchantEncounterRoutines : IMerchantEncounterRoutines
    {
        private IItemPresenter _itemPresenter;
        private GameBoardHolder _gameBoardHolder;

        public MerchantEncounterRoutines(
            IItemPresenter itemPresenter, 
            GameBoardHolder gameBoardHolder)
        {
            _itemPresenter = itemPresenter;
            _gameBoardHolder = gameBoardHolder;
        }

        public async UniTask ShowElements(MerchantEncounter encounter, IStoryContext storyContext, CancellationToken cancellationToken)
        {
            Sprite portrait = await encounter.MerchantPortrait.Load<Sprite>(cancellationToken);
            
            _gameBoardHolder.GameBoardComponent.EncounterBoard.EncounterView.Render(portrait);
            _gameBoardHolder.GameBoardComponent.EncounterBoard.SwitchToView(EncounterBoard.BoardType.Encounter);
        }

        public async UniTask ShowWares(IEnumerable<string> items, IStoryContext storyContext, CancellationToken cancellationToken)
        {
            ItemLineComponent encounterItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine;
            await _itemPresenter.ShowItems(encounterItemLine, items, cancellationToken);
        }

        public UniTask HideAll(CancellationToken cancellationToken)
        {
            _itemPresenter.RemoveEncounterItemsImmediate();
            
            _gameBoardHolder.GameBoardComponent.EncounterBoard.HideCurrentView();
            _gameBoardHolder.GameBoardComponent.EncounterBoard.EncounterView.Release();
            
            return UniTask.CompletedTask;
        }
        
        
    }
}