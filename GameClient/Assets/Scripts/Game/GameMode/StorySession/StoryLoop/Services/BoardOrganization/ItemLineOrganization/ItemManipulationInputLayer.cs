using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View;
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
        
        
        private bool _isActive = false;
        
        private bool _isSwipeStarted;
        private bool _isLongPressStarted;
        private bool _isSecondItemLineEngaged;
        
        private ItemContainerComponent _targetItem;
        private ItemLineComponent _originalItemLine;
        // it is called secondLine, but basically it is currently processed line, so it can be the same line as original
        private ItemLineComponent _secondItemLine; 

        private ItemLineBuffer _originalItemLineStateBuffer; // represent original state of line
        private ItemLineBuffer _secondItemLineBuffer; // represent original state of player line
        private ItemLineBuffer _itemLineWorkBuffer; // used as buffer for operations

        public ItemManipulationInputLayer(
            ICameraManager cameraManager, 
            IItemLineOrganizer itemLineOrganizer, 
            IItemManipulator itemManipulator, 
            GameBoardHolder gameBoardHolder)
        {
            _cameraManager = cameraManager;
            _itemLineOrganizer = itemLineOrganizer;
            _itemManipulator = itemManipulator;
            _gameBoardHolder = gameBoardHolder;

            _originalItemLineStateBuffer = new();
            _secondItemLineBuffer = new();
            _itemLineWorkBuffer = new();
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
            _itemLineOrganizer.RemoveItem(_originalItemLine, _targetItem);
            
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
            
            Vector3 worldPoint = _cameraManager.MainCamera.ScreenToWorldPoint(swipeUpdate.CurrentPosition);
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
            
            // add between lines item swap implementation here
            
            
            if (!_itemLineOrganizer.TryGetLineIndexForPosition(_secondItemLine, worldPoint, out int index))
            {
                // as we already located second line, there should not be case for getting in here but just in case
                return true;
            }

            _itemLineWorkBuffer.ClearBuffer();
            if (!_itemLineOrganizer.TryBuildItemConfiguration(_secondItemLineBuffer.ItemBuffer, _targetItem, ref index, ref _itemLineWorkBuffer.ItemBuffer))
            {
                return true;
            }
            
            _itemLineOrganizer.Organize(_secondItemLine, _itemLineWorkBuffer.ItemBuffer, true);
            
            return true;
        }
        
        private bool TryHandleSwipeEnd(SwipeEnd swipeEnd)
        {
            if (!_isSwipeStarted)
            {
                return false;
            }
            
            FinalizeItemMovement(Application.exitCancellationToken).Forget();
            
            
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
        
        private void TryUpdateLine()
        {
            
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

        private async UniTask FinalizeItemMovement(CancellationToken cancellationToken)
        {
            TryUpdateLine();
            
            _itemLineWorkBuffer.ClearBuffer();

            var isTransactionCompleted = await _itemManipulator.TryCompleteItemTransition(_originalItemLine,
                _secondItemLine, _targetItem, cancellationToken);
            
            if (_isSecondItemLineEngaged)
            {
                _secondItemLine = null;
                _isSecondItemLineEngaged = false;
                
                _originalItemLineStateBuffer.ClearBuffer();
                _secondItemLineBuffer.ClearBuffer();
                return;
            }

            _itemLineOrganizer.Organize(_originalItemLine, _originalItemLineStateBuffer.ItemBuffer, true);
            _originalItemLineStateBuffer.ClearBuffer();
            
            _targetItem.RenderEndMovement();
            
            _targetItem = null;
            
            _isSwipeStarted = false;
        }
        
    }
}