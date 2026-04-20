using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public interface IItemPresenter
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public UniTask ShowItems(List<string> itemIds, CancellationToken cancellationToken);
        public void CleanUp();
    }
}