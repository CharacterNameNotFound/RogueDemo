using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    public class ItemManipulator : IItemManipulator
    {
        private IItemLineOrganizer _lineOrganizer;
        private GameBoardHolder _gameBoardHolder;

        public ItemManipulator(IItemLineOrganizer lineOrganizer, GameBoardHolder gameBoardHolder)
        {
            _lineOrganizer = lineOrganizer;
            _gameBoardHolder = gameBoardHolder;
        }

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            _lineOrganizer.Initialize();
            return UniTask.CompletedTask;
        }

        public bool TryGetItemLineForItem(ItemContainerComponent item, out ItemLineComponent line)
        {
            foreach (ItemLineComponent itemLine in _gameBoardHolder.GameBoardComponent.ItemLineViewController.EnumerateItemLines())
            {
                if (_lineOrganizer.IsLocatedInItemLine(itemLine, item.transform.position))
                {
                    line = itemLine;
                    return true;
                }
                
            }

            line = null;
            return false;
        }

        public void CleanUp()
        {
            _lineOrganizer.CleanUp();
        }
    }
}