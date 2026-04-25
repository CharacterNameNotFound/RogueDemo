using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using GameWideSystems.CameraManagement;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    // ToDo: swipe handling over-bloats class,splint into classes, 
    public class ItemManipulationInputLayer : IInputHandlerLayer
    {
        public int Index => 1100;
        public InputType InputType => InputType.Pointer;
        
        private ICameraManager _cameraManager;
        private IItemLineOrganizer _itemLineOrganizer;
        private IItemManipulator _itemManipulator;
        private GameBoardHolder _gameBoardHolder;
        private InputControlFacade _inputControlFacade;
        
        
        private bool _isActive = false;
        
        private bool _isSwipeStarted;
        private bool _isLongPressStarted;
        private bool _isSecondItemLineEngaged;
        
        private ItemContainerComponent _targetItem;
        private ItemLineComponent _originalItemLine;
        // it is called secondLine, but basically it is currently processed line, so it can be the same line as original
        private ItemLineComponent _secondItemLine;
        private int _targetItemOriginalIndex;
        // displacement from mouse to object world pivot
        private Vector2 _mouseDragStartDisplacement;
        // displacement from mouse to item point that will serve as first entry in item line 
        private Vector2 _mouseDragStartToItemLinePivotDisplacement;

        private ItemLineBuffer _originalItemLineStateBuffer; // represent original state of line
        private ItemLineBuffer _secondItemLineBuffer; // represent original state of player line
        private ItemLineBuffer _itemLineWorkBuffer; // used as buffer for operations
        private ItemLineBuffer _secondItemLineWorkBuffer; // used as buffer for operations

        public ItemManipulationInputLayer(
            ICameraManager cameraManager, 
            IItemLineOrganizer itemLineOrganizer, 
            IItemManipulator itemManipulator, 
            GameBoardHolder gameBoardHolder, 
            InputControlFacade inputControlFacade)
        {
            _cameraManager = cameraManager;
            _itemLineOrganizer = itemLineOrganizer;
            _itemManipulator = itemManipulator;
            _gameBoardHolder = gameBoardHolder;
            _inputControlFacade = inputControlFacade;

            _originalItemLineStateBuffer = new();
            _secondItemLineBuffer = new();
            _itemLineWorkBuffer = new();
            _secondItemLineWorkBuffer = new();
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
                // show tooltip
                Tap tap => TryHandleTap(tap),
                
                // Details view
                LongPressStart longPressStart => TryHandleLongPressStart(longPressStart),
                LongPressUpdate longPressUpdate => TryHandleLongPressUpdate(longPressUpdate),
                LongPressEnd longPressEnd => TryHandleLongPressEnd(longPressEnd),
                
                // item movement
                Swipe swipe => TryHandleSwipeStart(swipe),
                SwipeUpdate swipeUpdate => TryHandleSwipeUpdate(swipeUpdate),
                SwipeEnd swipeEnd => TryHandleSwipeEnd(swipeEnd),
                _ => false
            };

        }
        
        
        // handling tooltip
        private bool TryHandleTap(Tap tap)
        {
            return true;
        }
        
        // item movement
        private bool TryHandleSwipeStart(Swipe swipe)
        {
            Vector3 worldPoint = _cameraManager.MainCamera.ScreenToWorldPoint(swipe.SourcePosition);
            
            if (!TryGetTarget(worldPoint, out _targetItem))
            {
                return false;
            }

            _originalItemLineStateBuffer.ClearBuffer();
            _originalItemLineStateBuffer.CopyFrom(_originalItemLine);
            _itemLineOrganizer.RemoveItem(_originalItemLine, _targetItem, out _targetItemOriginalIndex);

            _mouseDragStartDisplacement = _targetItem.transform.position - worldPoint;
            _mouseDragStartToItemLinePivotDisplacement = _targetItem.GetItemLinePivot() - worldPoint;
            
            _targetItem.RenderStarMovement();
            
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

            if (targetedLine != _secondItemLine)
            {
                DisengageSecondLine();
                EngageSecondLine(targetedLine);
            }
            

            _itemLineWorkBuffer.ClearBuffer();
            if (!_itemManipulator.TryUpdateItemLines(itemPivot, _originalItemLine, targetedLine, _secondItemLineBuffer, _targetItem, _itemLineWorkBuffer))
            {
                _itemLineOrganizer.Organize(_secondItemLine, _secondItemLineBuffer.ItemBuffer, true);
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
            
            
            return true;
        }
        

        // detailed view
        private bool TryHandleLongPressStart(LongPressStart longPressStart)
        {
            if (!TryGetTarget(longPressStart.SourcePosition, out _targetItem))
            {
                return false;
            }
            
            _isLongPressStarted = true;
            return true;
        }
        
        private bool TryHandleLongPressUpdate(LongPressUpdate longPressUpdate)
        {
            if (!_isLongPressStarted)
            {
                return false;
            }
            
            return _isLongPressStarted;
        }
        
        private bool TryHandleLongPressEnd(LongPressEnd longPressEnd)
        {
            if (!_isLongPressStarted)
            {
                return false;
            }
            
            _targetItem = null;
            
            _isLongPressStarted = false;
            return true;
        }

        
        // methods
        
        private void EngageSecondLine(ItemLineComponent secondLine)
        {
            _isSecondItemLineEngaged = true;
            
            _secondItemLineBuffer.ClearBuffer();
            _secondItemLineBuffer.CopyFrom(secondLine);
            
            _secondItemLine = secondLine;
        }
        
        private void DisengageSecondLine()
        {
            if (!_isSecondItemLineEngaged)
            {
                return;
            }
            
            _itemLineOrganizer.Organize(_secondItemLine, _secondItemLineBuffer.ItemBuffer, true);
            _secondItemLineBuffer.ClearBuffer();
            
            _isSecondItemLineEngaged = false;
            _secondItemLine = null;
        }
        
        private bool TryGetTarget(Vector3 coords, out ItemContainerComponent item)
        {
            Collider2D target = Physics2D.OverlapPoint(coords);

            if (target is null || !target.TryGetComponent(out item))
            {
                item = null;
                return false;
            }
            
            return _itemManipulator.TryGetItemLineForItem(item, out _originalItemLine);
        }

        private async UniTask FinalizeItemMovement(Vector3 mouseWorldPoint, CancellationToken cancellationToken)
        {
            Vector3 itemPivot = mouseWorldPoint + (Vector3) _mouseDragStartToItemLinePivotDisplacement;
            
            _inputControlFacade.SetInputsAvailable(false);
            
            _isSwipeStarted = false;
            
            _itemLineWorkBuffer.ClearBuffer();
            _secondItemLineWorkBuffer.ClearBuffer();
            
            if (_isSecondItemLineEngaged)
            {
                bool isTransactionSuccess = await _itemManipulator.TryCompleteItemTransition(
                    itemPivot,
                    _targetItemOriginalIndex,
                    _originalItemLine, 
                    _secondItemLine, 
                    _secondItemLineBuffer, 
                    _targetItem, 
                    _itemLineWorkBuffer,
                    _secondItemLineWorkBuffer,
                    cancellationToken);

                if (!isTransactionSuccess)
                {
                    _itemLineOrganizer.Organize(_originalItemLine, _originalItemLineStateBuffer.ItemBuffer, true);
                    _itemLineOrganizer.Organize(_secondItemLine, _secondItemLineBuffer.ItemBuffer, true);
                }
                
                _targetItem.RenderEndMovement();
                
                _secondItemLine = null;
                _isSecondItemLineEngaged = false;
                
                _itemLineWorkBuffer.ClearBuffer();
                _secondItemLineWorkBuffer.ClearBuffer();
                
                _originalItemLineStateBuffer.ClearBuffer();
                _secondItemLineBuffer.ClearBuffer();
                
                _inputControlFacade.SetInputsAvailable(true);
                return;
            }

            _itemLineOrganizer.Organize(_originalItemLine, _originalItemLineStateBuffer.ItemBuffer, true);
            _originalItemLineStateBuffer.ClearBuffer();
            
            _targetItem.RenderEndMovement();
            _targetItem = null;
            
            _inputControlFacade.SetInputsAvailable(true);
        }
        
    }
}