using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public interface IItemPresenter
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public UniTask ShowItems(ItemLineComponent itemLine, IEnumerable<string> itemIds, CancellationToken cancellationToken);
        public void UpdateItemRarityFrame(ItemContainerComponent itemContainerComponent);
        public void UpdateItemRarityFrame(ItemContainerComponent itemContainerComponent, ItemRarity rarity);
        public void RemoveItemsImmediate(IEnumerable<ItemContainerComponent> itemContainerComponents);
        public void RemoveEncounterItemsImmediate();
        public void CleanUp();
    }
}