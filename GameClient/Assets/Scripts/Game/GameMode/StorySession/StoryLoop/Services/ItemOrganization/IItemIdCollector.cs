using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public interface IItemIdCollector
    {
        public UniTask AppendItemHierarchy(Item item, HashSet<string> itemIds, CancellationToken cancellationToken);

    }
}