using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using UnityEngine;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles.Routines
{
    public class BattleEncounterRoutines : IBattleEncounterRoutines
    {
        private IItemPresenter _itemPresenter;
        private GameBoardHolder _gameBoardHolder;

        public BattleEncounterRoutines(
            IItemPresenter itemPresenter, 
            GameBoardHolder gameBoardHolder)
        {
            _itemPresenter = itemPresenter;
            _gameBoardHolder = gameBoardHolder;
        }

        public async UniTask ShowElements(BattleEncounter encounter, CancellationToken cancellationToken)
        {
            Sprite portrait = await encounter.Portrait.Load<Sprite>(cancellationToken);
            
            _gameBoardHolder.GameBoardComponent.EncounterBoard.BattleView.Render(portrait);
            _gameBoardHolder.GameBoardComponent.EncounterBoard.SwitchToView(EncounterBoard.BoardType.Battle);
        }

        public async UniTask LoadItemsUpdateViews(IEnumerable<string> items, CancellationToken cancellationToken)
        {
            ItemLineComponent encounterItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine;
            await _itemPresenter.ShowItems(encounterItemLine, items, cancellationToken);

            
            
            
            
        }

        public UniTask HideAll(CancellationToken cancellationToken)
        {
            // opponent items are "ephemeral"
            _itemPresenter.RemoveEncounterItemsImmediate(true);
            
            _gameBoardHolder.GameBoardComponent.EncounterBoard.HideCurrentView();
            _gameBoardHolder.GameBoardComponent.EncounterBoard.BattleView.Release();
            
            return UniTask.CompletedTask;
        }
        
    }
}