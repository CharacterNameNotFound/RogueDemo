using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public interface IItemPresenter
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public UniTask ShowItems(IEnumerable<string> itemIds, CancellationToken cancellationToken);
        public void RemoveItemsImmediate(IEnumerable<ItemContainerComponent> itemContainerComponents);
        public void RemoveEncounterItemsImmediate();
        public void CleanUp();
    }
}