using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using GameWideSystems.CameraManagement;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    // ToDo: swipe handling over-bloats class, splint into classes, reduce nesting
    public class ItemManipulationInputLayer : IInputHandlerLayer
    {
        public int Index => 1100;
        public InputType InputType => InputType.Pointer;
        
        private ICameraManager _cameraManager;
        private IItemLineOrganizer _itemLineOrganizer;
        private IItemManipulator _itemManipulator;
        private GameBoardHolder _gameBoardHolder;
        private InputControlFacade _inputControlFacade;
        private IItemTransactionOperationController _itemTransactionOperationController;
        
        
        private bool _isActive = false;
        
        private bool _isSwipeStarted;
        private bool _isSecondItemLineEngaged;
        
        private ItemContainerComponent _targetItem;
        private ItemLineComponent _originalItemLine;
        private ItemLineComponent _targetItemLine;
        private int _targetItemOriginalIndex;
        // displacement from mouse to object world pivot
        private Vector2 _mouseDragStartDisplacement;
        // displacement from mouse to item point that will serve as first entry in item line 
        private Vector2 _mouseDragStartToItemLinePivotDisplacement;

        private ItemLineBuffer _originalItemLineStateBuffer; // represent original state of line
        private ItemLineBuffer _targetItemLineBuffer; // represent original state of target line
        private ItemLineBuffer _itemLineWorkBuffer; // used as buffer for operations
        private ItemLineBuffer _targetItemLineWorkBuffer; // used as buffer for operations

        public ItemManipulationInputLayer(
            ICameraManager cameraManager, 
            IItemLineOrganizer itemLineOrganizer, 
            IItemManipulator itemManipulator, 
            GameBoardHolder gameBoardHolder, 
            InputControlFacade inputControlFacade, 
            IItemTransactionOperationController itemTransactionOperationController)
        {
            _cameraManager = cameraManager;
            _itemLineOrganizer = itemLineOrganizer;
            _itemManipulator = itemManipulator;
            _gameBoardHolder = gameBoardHolder;
            _inputControlFacade = inputControlFacade;
            _itemTransactionOperationController = itemTransactionOperationController;

            _originalItemLineStateBuffer = new();
            _targetItemLineBuffer = new();
            _itemLineWorkBuffer = new();
            _targetItemLineWorkBuffer = new();
        }
        

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public bool TryHandle(IGesture gesture)
        {
            if (!_isActive)
            {
                return false;
            }

            return gesture switch
            {
                // item movement
                Swipe swipe => TryHandleSwipeStart(swipe),
                SwipeUpdate swipeUpdate => TryHandleSwipeUpdate(swipeUpdate),
                SwipeEnd swipeEnd => TryHandleSwipeEnd(swipeEnd),
                _ => false
            };

        }
        
        
        // item movement
        private bool TryHandleSwipeStart(Swipe swipe)
        {
            Vector3 worldPoint = _cameraManager.MainCamera.ScreenToWorldPoint(swipe.SourcePosition);
            
            if (!TryGetTarget(worldPoint, out _targetItem, out _originalItemLine))
            {
                return false;
            }

            if (!_itemTransactionOperationController.CanMoveItem(_targetItem, _originalItemLine))
            {
                _targetItem = null;
                _originalItemLine = null;
                return false;
            }

            _originalItemLineStateBuffer.ClearBuffer();
            _originalItemLineStateBuffer.CopyFrom(_originalItemLine);
            _itemLineOrganizer.RemoveItem(_originalItemLine, _targetItem, out _targetItemOriginalIndex);

            _mouseDragStartDisplacement = _targetItem.transform.position - worldPoint;
            _mouseDragStartToItemLinePivotDisplacement = _targetItem.GetItemLinePivot() - worldPoint;
            
            _targetItem.RenderStarMovement();
            _gameBoardHolder.GameBoardComponent.EncounterBoard.ToggleSellFirm(true);
            
            _isSwipeStarted = true;
            return true;
        }

        private bool TryHandleSwipeUpdate(SwipeUpdate swipeUpdate)
        {
            if (!_isSwipeStarted)
            {
                return false;
            }
            
            Vector3 mouseWorldPoint = _cameraManager.MainCamera.ScreenToWorldPoint(swipeUpdate.CurrentPosition);
            Vector3 itemPivot = mouseWorldPoint + (Vector3) _mouseDragStartToItemLinePivotDisplacement;
            Vector3 worldPoint = mouseWorldPoint + (Vector3) _mouseDragStartDisplacement;
            
            _targetItem.transform.position = new Vector3(worldPoint.x, worldPoint.y, _targetItem.transform.position.z);
            
            if (!_itemManipulator.TryGetItemLineForItem(_targetItem, out ItemLineComponent targetedLine))
            {
                if (_isSecondItemLineEngaged)
                {
                    DisengageSecondLine();
                }
                
                return true;
            }

            if (!targetedLine.IsPlayerModifyAvailable)
            {
                return true;
            }

            if (targetedLine != _targetItemLine)
            {
                DisengageSecondLine();
                EngageSecondLine(targetedLine);
            }

            // no need to change line if there is container with upgradable item
            if (_itemTransactionOperationController.CanUpgrade(_targetItem, _targetItemLine))
            {
                return true;
            }

            _itemLineWorkBuffer.ClearBuffer();
            if (!_itemManipulator.TryUpdateItemLines(itemPivot, _originalItemLine, targetedLine, _targetItemLineBuffer, _targetItem, _itemLineWorkBuffer))
            {
                _itemLineOrganizer.Organize(_targetItemLine, _targetItemLineBuffer.ItemBuffer, true);
            }
            
            return true;
        }
        
        private bool TryHandleSwipeEnd(SwipeEnd swipeEnd)
        {
            if (!_isSwipeStarted)
            {
                return false;
            }
            
            Vector3 worldPoint = _cameraManager.MainCamera.ScreenToWorldPoint(swipeEnd.CurrentPosition);
            _targetItem.transform.position = new Vector3(worldPoint.x, worldPoint.y, _targetItem.transform.position.z);
            
            FinalizeItemMovement(worldPoint, Application.exitCancellationToken).Forget();
            _gameBoardHolder.GameBoardComponent.EncounterBoard.ToggleSellFirm(false);
            
            return true;
        }
        

        
        // methods
        
        private void EngageSecondLine(ItemLineComponent secondLine)
        {
            _isSecondItemLineEngaged = true;
            
            _targetItemLineBuffer.ClearBuffer();
            _targetItemLineBuffer.CopyFrom(secondLine);
            
            _targetItemLine = secondLine;
        }
        
        private void DisengageSecondLine()
        {
            if (!_isSecondItemLineEngaged)
            {
                return;
            }
            
            _itemLineOrganizer.Organize(_targetItemLine, _targetItemLineBuffer.ItemBuffer, true);
            _targetItemLineBuffer.ClearBuffer();
            
            _isSecondItemLineEngaged = false;
            _targetItemLine = null;
        }
        
        private bool TryGetTarget(
            Vector3 coords, 
            out ItemContainerComponent item,
            out ItemLineComponent originalItemLine)
        {
            Collider2D target = Physics2D.OverlapPoint(coords);

            if (target is null || !target.TryGetComponent(out item))
            {
                item = null;
                originalItemLine = null;
                return false;
            }
            
            return _itemManipulator.TryGetItemLineForItem(item, out originalItemLine);
        }

        private async UniTask FinalizeItemMovement(Vector3 mouseWorldPoint, CancellationToken cancellationToken)
        {
            Vector3 itemPivot = mouseWorldPoint + (Vector3) _mouseDragStartToItemLinePivotDisplacement;
            
            _inputControlFacade.SetInputsAvailable(false);
            
            _isSwipeStarted = false;
            
            _itemLineWorkBuffer.ClearBuffer();
            _targetItemLineWorkBuffer.ClearBuffer();
            
            if (_isSecondItemLineEngaged)
            {
                bool isTransactionSuccess = await _itemManipulator.TryCompleteItemTransition(
                    itemPivot,
                    _targetItemOriginalIndex,
                    _originalItemLine, 
                    _targetItemLine, 
                    _targetItemLineBuffer, 
                    _targetItem, 
                    _itemLineWorkBuffer,
                    _targetItemLineWorkBuffer,
                    cancellationToken);

                if (!isTransactionSuccess)
                {
                    _itemLineOrganizer.Organize(_originalItemLine, _originalItemLineStateBuffer.ItemBuffer, true);
                    _itemLineOrganizer.Organize(_targetItemLine, _targetItemLineBuffer.ItemBuffer, true);
                }
                
                _targetItem.RenderEndMovement();
                
                _targetItemLine = null;
                _isSecondItemLineEngaged = false;
                
                _itemLineWorkBuffer.ClearBuffer();
                _targetItemLineWorkBuffer.ClearBuffer();
                
                _originalItemLineStateBuffer.ClearBuffer();
                _targetItemLineBuffer.ClearBuffer();
                
                _inputControlFacade.SetInputsAvailable(true);
                return;
            }

            bool isSold = await _itemManipulator.TrySellItem(mouseWorldPoint, _originalItemLineStateBuffer.ItemBuffer, _originalItemLine, _targetItem, cancellationToken);

            _itemLineOrganizer.Organize(_originalItemLine, _originalItemLineStateBuffer.ItemBuffer, true);
            _originalItemLineStateBuffer.ClearBuffer();

            if (!isSold)
            {
                _targetItem.RenderEndMovement();
            }
            
            _targetItem = null;
            
            _inputControlFacade.SetInputsAvailable(true);
        }
        
    }
}