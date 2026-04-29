using System.Threading;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.UI.Tooltips;
using GameWideSystems.InputManager;
using GameWideSystems.InputManager.GestureReaders.Pointer;
using GameWideSystems.TooltipsManagement;
using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public class ItemDetailsInputLayers : IInputHandlerLayer
    {
        public int Index => 1101;
        public InputType InputType => InputType.Pointer;

        private ITooltipManager _tooltipManager;
        
        private bool _isActive;
        
        private ItemContainerComponent _targetItem;
        private bool _isLongPressStarted;


        public ItemDetailsInputLayers(ITooltipManager tooltipManager)
        {
            _tooltipManager = tooltipManager;
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
                
                _ => false
            };
        }
        
        // handling tooltip
        private bool TryHandleTap(Tap tap)
        {
            if (!TryGetTarget(tap.SourcePosition, out _targetItem))
            {
                return false;
            }
            
            // show tooltip
            //_tooltipManager.ShowTooltip<TextTooltip>(TooltipType.GenericText, new TextTooltip(), new CancellationToken());
            
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

        
    }
}