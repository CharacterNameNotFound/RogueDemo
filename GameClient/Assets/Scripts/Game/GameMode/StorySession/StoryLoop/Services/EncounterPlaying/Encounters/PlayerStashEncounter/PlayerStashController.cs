using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board;
using GameWideSystems.InputManager;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.PlayerStashEncounter
{
    public class PlayerStashController : IPlayerStashController
    {
        private GameBoardHolder _gameBoardHolder;
        private InputControlFacade _inputControlFacade;

        private EncounterBoard.BoardType? _preInventoryBoard;

        public PlayerStashController(InputControlFacade inputControlFacade, GameBoardHolder gameBoardHolder)
        {
            _inputControlFacade = inputControlFacade;
            _gameBoardHolder = gameBoardHolder;
        }

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            _gameBoardHolder.GameBoardComponent.GameBoardInteractables.InventoryButton.OnPress += ToggleInventory;
            
            return UniTask.CompletedTask;
        }
        
        public void CleanUp()
        {
            _gameBoardHolder.GameBoardComponent.GameBoardInteractables.InventoryButton.OnPress -= ToggleInventory;
        }

        private void ToggleInventory()
        {
            EncounterBoard encounterBoard = _gameBoardHolder.GameBoardComponent.EncounterBoard;
            
            if (encounterBoard.GetCurrentView() != EncounterBoard.BoardType.Stash)
            {
                OpenInventory();
                return;
            }
            
            CloseInventory();
        }

        private void OpenInventory()
        {
            EncounterBoard encounterBoard = _gameBoardHolder.GameBoardComponent.EncounterBoard;

            _preInventoryBoard = encounterBoard.GetCurrentView();
            encounterBoard.SwitchToView(EncounterBoard.BoardType.Stash);
            _gameBoardHolder.GameBoardComponent.ItemLineViewController.SwapOppositeItemLine(ItemLineViewController.OppositeItemLineType.Stash);
        }

        private void CloseInventory()
        {
            _gameBoardHolder.GameBoardComponent.EncounterBoard.SwitchToView(_preInventoryBoard);
            _gameBoardHolder.GameBoardComponent.ItemLineViewController.SwapOppositeItemLine(ItemLineViewController.OppositeItemLineType.Encounter);
        }
        
    }
}