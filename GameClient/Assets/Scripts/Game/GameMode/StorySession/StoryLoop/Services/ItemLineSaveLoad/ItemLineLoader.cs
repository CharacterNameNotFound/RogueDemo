using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.BoardModelManipulation;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemLineSaveLoad
{
    public class ItemLineLoader : IItemLineLoader
    {
        private IItemPresenter _itemPresenter;
        private GameBoardHolder _gameBoardHolder;
        private IBoardModelManipulator _boardModelManipulator;

        public ItemLineLoader(
            IItemPresenter itemPresenter, 
            GameBoardHolder gameBoardHolder, 
            IBoardModelManipulator boardModelManipulator)
        {
            _itemPresenter = itemPresenter;
            _gameBoardHolder = gameBoardHolder;
            _boardModelManipulator = boardModelManipulator;
        }

        public async UniTask Load(ItemLineSaveData playerSaveData, CancellationToken cancellationToken)
        {
            ItemLineComponent player = _gameBoardHolder.GameBoardComponent.ItemLineViewController.PlayerItemLine;
            ItemLineComponent stash = _gameBoardHolder.GameBoardComponent.ItemLineViewController.InventoryItemLine;
            
            await _itemPresenter.ShowItems(player, playerSaveData.PlayerLine, cancellationToken);
            await _itemPresenter.ShowItems(stash, playerSaveData.StashLine, cancellationToken);
            
            _boardModelManipulator.UpdatePlayerLines();
        }
        
        
    }
    
}