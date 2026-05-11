using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public interface IItemIdCollector
    {
        public UniTask AppendItemHierarchy(string itemId, HashSet<string> itemIds, CancellationToken cancellationToken);
        public UniTask AppendItemHierarchy(Item item, HashSet<string> itemIds, CancellationToken cancellationToken);

    }
}