using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public interface IItemTransactionOperationController
    {
        public bool IsPurchaseAttempt(ItemContainerComponent item, ItemLineComponent originalItemLine);
        public bool CanMoveItem(ItemContainerComponent item, ItemLineComponent originalItemLine);
        public bool CanSellItem(ItemContainerComponent item, ItemLineComponent originalItemLine);
        
        public bool CanUpgrade(
            ItemContainerComponent item, 
            ItemLineComponent originalItemLineout, 
            out ItemLineComponent upgradableLine, 
            out ItemContainerComponent upgradableItem);
        
        public bool CanPurchase(ItemContainerComponent item, ItemLineComponent originalItemLine);
        
    }
}