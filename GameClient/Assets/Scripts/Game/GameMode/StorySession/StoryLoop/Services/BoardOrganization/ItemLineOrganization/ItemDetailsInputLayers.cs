using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemDescriptionBuilding;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.UI.Tooltips;
using GameWideSystems.CameraManagement;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using GameWideSystems.TooltipsManagement;
using UnityEngine;
using Utils.UtilityTypes.Counters;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public class ItemDetailsInputLayers : IInputHandlerLayer
    {
        public int Index => 1101;
        public InputType InputType => InputType.Pointer;

        private ITooltipManager _tooltipManager;
        private InputControlFacade _inputControlFacade;
        private IItemDescriptionBuilder _itemDescriptionBuilder;
        private ICameraManager _cameraManager;
        
        private CounterLock _counterLock = new(true);
        
        private ItemContainerComponent _targetItem;
        private bool _isLongPressStarted;


        public ItemDetailsInputLayers(
            ITooltipManager tooltipManager, 
            InputControlFacade inputControlFacade, 
            ICameraManager cameraManager, 
            IItemDescriptionBuilder itemDescriptionBuilder)
        {
            _tooltipManager = tooltipManager;
            _inputControlFacade = inputControlFacade;
            _cameraManager = cameraManager;
            _itemDescriptionBuilder = itemDescriptionBuilder;
        }
        
        public void SetActive(bool isActive)
        {
            _counterLock.Toggle(isActive);
        }
        
        public bool TryHandle(IGesture gesture)
        {
            if (_counterLock.IsLocked())
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
                
                _ => false
            };
        }
        
        // handling tooltip
        private bool TryHandleTap(Tap tap)
        {
            Vector3 worldPoint = _cameraManager.MainCamera.ScreenToWorldPoint(tap.SourcePosition);
            if (!TryGetTarget(worldPoint, out _targetItem))
            {
                return false;
            }
            
            ShowTooltip(_targetItem, tap.SourcePosition).Forget();
            
            
            return true;
        }

        // detailed view
        private bool TryHandleLongPressStart(LongPressStart longPressStart)
        {
            if (!TryGetTarget(longPressStart.SourcePosition, out _targetItem))
            {
                return false;
            }
            
            //ToDo: detailed view
            Debug.Log($"Detailed item view shown: {_targetItem.StoredItem.ItemId}");

            
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
        
        private bool TryGetTarget(
            Vector3 coords, 
            out ItemContainerComponent item)
        {
            Collider2D target = Physics2D.OverlapPoint(coords);

            if (target is null || !target.TryGetComponent(out item))
            {
                item = null;
                return false;
            }

            return true;
        }

        private async UniTask ShowTooltip(ItemContainerComponent targetItem, Vector2 tapSourcePosition)
        {
            // preventing spam of tooltips
            _inputControlFacade.SetInputsAvailable(false);
            
            string itemName = _itemDescriptionBuilder.GetItemName(targetItem.StoredItem);
            string itemDescription = _itemDescriptionBuilder.GetItemDescription(targetItem.StoredItem);
            
            await _tooltipManager.ShowTooltip<TextTooltip>(TooltipType.GenericText, new TextTooltipParams(itemName, itemDescription, tapSourcePosition), Application.exitCancellationToken);
            
            _inputControlFacade.SetInputsAvailable(true);
        } 
        
    }
}